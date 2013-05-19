using System;

namespace Ark.Pipes {
    //ProviderHolder<T> is different from ReadableVariable<Provider<T>> because it inherits from Provider<T>, not Provider<Provider<T>>
    public class ProviderHolder<T> : Provider<T> {
        protected Provider<T> _provider;
#if !NOTIFICATIONS_DISABLE
        protected PrivateNotifier _notifier = new PrivateNotifier();
        public event Action ProviderChanged;
#endif

        public ProviderHolder(Provider<T> provider) {
            _provider = provider;
#if !NOTIFICATIONS_DISABLE
            _notifier.Source = _provider.Notifier;
#endif
        }

        public ProviderHolder(Provider<T> provider, out Action<Provider<T>> changer)
            : this(provider) {
            changer = SetProvider;
        }

        protected void SetProvider(Provider<T> value) {
            if (value != _provider) {
                _provider = value;
#if !NOTIFICATIONS_DISABLE
                _notifier.Source = _provider.Notifier;
                SignalProviderChanged();
                _notifier.SignalValueChanged();
#endif
            }
        }

#if !NOTIFICATIONS_DISABLE
        void SignalProviderChanged() {
            var handler = ProviderChanged;
            if (handler != null) {
                handler();
            }
        }
#endif

        public override T GetValue() {
            return _provider.GetValue();
        }
    }

    public sealed class Property<T> : ProviderHolder<T>, IIn<T>, IIn<Provider<T>>, 
#if NOTIFICATIONS_DISABLE
        IOut<Provider<T>>
#else
        INotifyingOut<Provider<T>>
#endif
    {
        public Property() : base(Constant<T>.Default) { }

        public Property(T value) : base(new Constant<T>(value)) { }

        public Property(Provider<T> provider) : base(provider) { }

        public new T Value {
            get { return base.GetValue(); }
            set { Provider = new Constant<T>(value); }
        }

        public Provider<T> Provider {
            get { return _provider; }
            set { SetProvider(value); }
        }

        public Provider<T> AsReadOnly() {
            return new ProviderHolder<T>(this);
        }

        void IIn<T>.SetValue(T value) {
            Provider = new Constant<T>(value);
        }

        void IIn<Provider<T>>.SetValue(Provider<T> value) {
            Provider = value;
        }

        Provider<T> IOut<Provider<T>>.GetValue() {
            return _provider;
        }

        [System.Runtime.CompilerServices.SpecialName]
        public static Property<T> op_Implicit(Provider<T> provider) {
            return new Property<T>(provider);
        }

        public static implicit operator T(Property<T> property) {
            return property.Value;
        }

        public static implicit operator Property<T>(T value) {
            return new Property<T>(value);
        }
    }
}

//TODO:
//Provider "stack" (Property Value Precedence slots  http://msdn.microsoft.com/en-us/library/ms743230.aspx )
//Property binder that doesn't require  instance
//Data context
//Attached properties
//INotifyPropertyChanged    

