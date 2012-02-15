using System.ComponentModel;
using System.Windows;

namespace Ark.Pipes.Wpf {
    //DP that reads from Provider
    public class DependencyPropertyAdapter<T> : DependencyObject, INotifyPropertyChanged {
        const string _propertyName = "Value";
        public event PropertyChangedEventHandler PropertyChanged;
        public static readonly DependencyProperty ValueProperty;
        private static readonly DependencyPropertyKey ValuePropertyKey;

        Property<T> _value;

        static DependencyPropertyAdapter() {
            ValuePropertyKey = DependencyProperty.RegisterReadOnly(_propertyName, typeof(T), typeof(DependencyPropertyAdapter<T>), null);
            ValueProperty = ValuePropertyKey.DependencyProperty;
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
            SetValue(ValuePropertyKey, _value.GetValue());
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(_propertyName));
            }
        }
    }
}
