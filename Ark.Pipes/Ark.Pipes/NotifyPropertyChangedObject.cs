using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#if !NOTIFICATIONS_DISABLE
namespace Ark.Pipes {
    [DefaultProperty("Value")]
    public sealed class NotifyPropertyChangedObject : CustomTypeDescriptor, INotifyPropertyChanged {
        Dictionary<string, IHasValue> _properties = new Dictionary<string, IHasValue>();
        PropertyDescriptorCollection _descriptors = new PropertyDescriptorCollection(null);

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddProperty<T>(string propertyName, INotifyingOut<T> provider) {
            var property = new Retranslator<T>(provider, this, propertyName);
            _properties.Add(propertyName, property);

            var descriptor = new Descriptor<T>(propertyName);
            _descriptors.Add(descriptor);
        }

        void SignalPropertyChanged(PropertyChangedEventArgs eventArgs) {
            var handlers = PropertyChanged;
            if (handlers != null) {
                handlers.Invoke(this, eventArgs);
            }
        }

        public object this[string propertyName] {
            get {
                return GetValue(propertyName);
            }
        }

        public object GetValue(string propertyName) {
            IHasValue property;
            if (!_properties.TryGetValue(propertyName, out property)) {
                throw new KeyNotFoundException(string.Format("Property \"{0}\" not found.", propertyName));
            }
            return property.GetValue();
        }

        public T GetValue<T>(string propertyName) {
            IHasValue property;
            if (!_properties.TryGetValue(propertyName, out property)) {
                throw new KeyNotFoundException(string.Format("Property \"{0}\" not found.", propertyName));
            }
            var typedProperty = property as Retranslator<T>;
            if (typedProperty == null) {
                throw new ArgumentException("T");
            }
            return typedProperty.GetTypedValue();
        }

        public void Dispose() {
            foreach (var disposable in _properties.Values.OfType<IDisposable>()) {
                disposable.Dispose();
            }
        }

        public override PropertyDescriptorCollection GetProperties() {
            return _descriptors;
        }

        interface IHasValue {
            object GetValue();
        }

        sealed class Descriptor<T> : PropertyDescriptor {
            string _propertyName;

            public Descriptor(string propertyName)
                : base(propertyName, null) {
                _propertyName = propertyName;
            }

            public override bool CanResetValue(object component) {
                return false;
            }

            public override object GetValue(object component) {
                return ((NotifyPropertyChangedObject)component).GetValue(_propertyName);
            }

            public override Type ComponentType {
                get { return typeof(NotifyPropertyChangedObject); }
            }

            public override bool IsReadOnly {
                get { return true; }
            }

            public override Type PropertyType {
                get { return typeof(T); }
            }

            public override void ResetValue(object component) { }

            public override void SetValue(object component, object value) { }

            public override bool ShouldSerializeValue(object component) { return false; }
        }

        sealed class Retranslator<T> : IHasValue, IDisposable, IValueChangeListener {
            INotifyingOut<T> _input;
            NotifyPropertyChangedObject _output;
            PropertyChangedEventArgs _eventArgs;

            public Retranslator(INotifyingOut<T> input, NotifyPropertyChangedObject output, string propertyName) {
                _input = input;
                _output = output;
                _eventArgs = new PropertyChangedEventArgs(string.Format("Value[\"{0}\"]", propertyName)); //TODO: Check whether this actually works.
                _input.Notifier.AddListener(this);
            }

            public object GetValue() {
                return _input.GetValue();
            }

            public T GetTypedValue() {
                return _input.GetValue();
            }

            void IValueChangeListener.OnValueChanged() {
                _output.SignalPropertyChanged(_eventArgs);
            }

            public void Dispose() {
                _input.Notifier.RemoveListener(this);
                _input = null;
                _output = null;
            }
        }
    }
}

namespace Ark.Pipes {
    public static partial class Extensions {
        public static NotifyPropertyChangedObject AsINotifyPropertyChanged<T>(this INotifyingOut<T> provider, string propertyName = "value") {
            var obj = new NotifyPropertyChangedObject();
            obj.AddProperty(propertyName, provider);
            return obj;
        }

        //public static NotifyPropertyChangedObject AsINotifyPropertyChanged<T>(this IEnumerable<KeyValuePair<string, INotifyingOut<T>> namedProviders) {
        //    var obj = new NotifyPropertyChangedObject();
        //    foreach(var kv in namedProviders) {
        //        obj.AddProperty(kv.Key, kv.Value);
        //    }
        //    return obj;
        //}        
    }
}
#endif