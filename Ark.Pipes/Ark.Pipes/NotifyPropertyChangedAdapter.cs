using System.ComponentModel;

namespace Ark.Pipes.Wpf {
    public class NotifyPropertyChangedAdapter<T> : INotifyPropertyChanged {
        const string _propertyName = "Value";
        NotifyingProvider<T> _value;
        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyPropertyChangedAdapter(NotifyingProvider<T> provider) {
            _value = provider;
            _value.ValueChanged += OnPropertyChanged;
            OnPropertyChanged();
        }

        public NotifyingProvider<T> Value {
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
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(_propertyName));
            }
        }
    }
}
