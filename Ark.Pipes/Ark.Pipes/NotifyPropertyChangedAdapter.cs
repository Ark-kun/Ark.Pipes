﻿using System.ComponentModel;

namespace Ark.Pipes.Wpf {
    public class NotifyPropertyChangedAdapter<T> : INotifyPropertyChanged {
        const string _propertyName = "Value";
        Property<T> _value;
        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyPropertyChangedAdapter(Provider<T> provider) {
            _value = new Property<T>(provider);
            _value.ValueChanged += OnPropertyChanged;
            OnPropertyChanged();
        }

        public Property<T> Value {
            get { return _value; }
            set { _value.Provider = value; }
        }

        protected void OnPropertyChanged() {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(_propertyName));
            }
        }
    }
}
