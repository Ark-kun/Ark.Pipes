using System.ComponentModel;
using System.Windows;
using Ark.Pipes;

namespace Ark.Wpf { //.Pipes.Wpf {
    //DP that reads from Provider
    public class DependencyPropertyAdapter<T> : DependencyObject, INotifyPropertyChanged {
        const string _propertyName = "Value";
        public event PropertyChangedEventHandler PropertyChanged;
        public static readonly DependencyProperty ValueProperty;
#if !SILVERLIGHT
        private static readonly DependencyPropertyKey ValuePropertyKey;
#endif

        Property<T> _value;

        static DependencyPropertyAdapter() {
#if SILVERLIGHT
            ValueProperty = DependencyProperty.Register(_propertyName, typeof(T), typeof(DependencyPropertyAdapter<T>), null);
#else
            ValuePropertyKey = DependencyProperty.RegisterReadOnly(_propertyName, typeof(T), typeof(DependencyPropertyAdapter<T>), null);
            ValueProperty = ValuePropertyKey.DependencyProperty;
#endif
        }

        public DependencyPropertyAdapter()
            : this(Constant<T>.Default) {
        }

        public DependencyPropertyAdapter(Provider<T> provider) {
            _value = new Property<T>(provider);
            _value.Notifier.ValueChanged += OnPropertyChanged;
            OnPropertyChanged();
        }

        public Property<T> Value {
            get { return _value; }
            set { _value.Provider = value;}
        }

        protected void OnPropertyChanged() {
#if SILVERLIGHT
            SetValue(ValueProperty, _value.GetValue());
#else
            SetValue(ValuePropertyKey, _value.GetValue());
#endif
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(_propertyName));
            }
        }
    }
}
