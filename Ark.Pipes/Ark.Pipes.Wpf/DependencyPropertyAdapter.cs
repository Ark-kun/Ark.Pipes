using System.ComponentModel;
using System.Windows;

namespace Ark.Pipes.Wpf {
    //DP that reads from Provider
    public class DependencyPropertyAdapter<T> : DependencyObject, INotifyPropertyChanged {
        const string _propertyName = "Value";
        public event PropertyChangedEventHandler PropertyChanged;
        public static readonly DependencyProperty ValueProperty;
        private static readonly DependencyPropertyKey ValuePropertyKey;

        Provider<T> _value;

        static DependencyPropertyAdapter() {
            ValuePropertyKey = DependencyProperty.RegisterReadOnly(_propertyName, typeof(T), typeof(DependencyPropertyAdapter<T>), null);
            ValueProperty = ValuePropertyKey.DependencyProperty;
        }

        public DependencyPropertyAdapter()
            : this(Constant<T>.Default) {
        }

        public DependencyPropertyAdapter(Provider<T> provider) {
            _value = provider;
            _value.ValueChanged += OnPropertyChanged;
            OnPropertyChanged();
        }

        public Provider<T> Value {
            get { return _value; }
            set {
                if (value != _value) {
                    _value.ValueChanged -= OnPropertyChanged;
                    _value = value;
                    _value.ValueChanged += OnPropertyChanged;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged() {
            SetValue(ValuePropertyKey, _value.GetValue());
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(_propertyName));
            }
        }
    }
}
