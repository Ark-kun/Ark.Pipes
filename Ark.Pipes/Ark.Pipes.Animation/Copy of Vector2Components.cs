using System;

namespace Ark.Pipes {
    //public class Vector2Components {
    //}
    public class Vector2ComponentsExp<TCreator, TProvider, TProperty> : INeedsCreator<double, TCreator, TProvider, TProperty>
        where TCreator : ICreator<double, TProvider, TProperty>, new()
        where TProvider : Provider<double>
        where TProperty : TProvider, IIn<Provider<double>> {
        TProperty _x;
        TProperty _y;
        TProvider _length;
        static TCreator _ctor = new TCreator();

        public Vector2ComponentsExp()
            : this(_ctor.Create(), _ctor.Create()) {
        }

        public Vector2ComponentsExp(TProvider x, TProvider y) {
            _x = _ctor.CreateProperty(x);
            _y = _ctor.CreateProperty(y);
            _length = _ctor.Create<double, double>((X, Y) => Math.Sqrt(X * X + Y * Y));
            //_length = new Function<double, double, double>((X, Y) => Math.Sqrt(X * X + Y * Y), _x, _y);
        }

        public TProvider Length {
            get { return _length; }
        }

        public TProvider X {
            get { return _x; }
            set { _x.SetValue(value); }
        }

        public TProvider Y {
            get { return _y; }
            set { _y.SetValue(value); }
        }
    }

    public interface INeedsCreator<TResult, TCreator, TProvider, TProperty>
        where TCreator : ICreator<TResult, TProvider, TProperty>, new()
        where TProvider : Provider<TResult>
        where TProperty : TProvider, IIn<Provider<TResult>> {
    }

    public interface ICreator<TResult, TProvider, TProperty>
        where TProvider : Provider<TResult>
        where TProperty : TProvider, IIn<Provider<TResult>> {
        TProvider Create();
        TProvider Create(TResult value);
        //TProvider Create(Func<TResult> func);
        TProvider Create<T>(Func<T, TResult> func);
        TProvider Create<T1, T2>(Func<T1, T2, TResult> func);
        TProperty CreateProperty(TProvider provider);
    }

    public class NormalCreator<TResult> : ICreator<TResult, Provider<TResult>, Property<TResult>> {
        public Provider<TResult> Create() {
            return Constant<TResult>.Default;
        }

        public Provider<TResult> Create(TResult value) {
            return Provider<TResult>.Create(value);
        }

        //public Provider<TResult> Create(Func<TResult> func) {
        //    return new  Function<TResult>(func);
        //}

        public Provider<TResult> Create<T>(Func<T, TResult> func) {
            return new Function<T, TResult>(func);
        }

        public Provider<TResult> Create<T1, T2>(Func<T1, T2, TResult> func) {
            return new Function<T1, T2, TResult>(func);
        }

        public Property<TResult> CreateProperty(Provider<TResult> provider) {
            return new Property<TResult>(provider);
        }
    }


    public class NotifyingCreator<TResult> : ICreator<TResult, NotifyingProvider<TResult>, NotifyingProperty<TResult>> {
        public NotifyingProvider<TResult> Create() {
            return Constant<TResult>.Default;
        }

        public NotifyingProvider<TResult> Create(TResult value) {
            return NotifyingProvider<TResult>.Create(value);
        }

        //public NotifyingProvider<TResult> Create(Func<TResult> func) {
        //    return new NotifyingFunction<TResult>(func);
        //}

        public NotifyingProvider<TResult> Create<T>(Func<T, TResult> func) {
            return new NotifyingFunction<T, TResult>(func);
        }

        public NotifyingProvider<TResult> Create<T1, T2>(Func<T1, T2, TResult> func) {
            return new NotifyingFunction<T1, T2, TResult>(func);
        }

        public NotifyingProperty<TResult> CreateProperty(NotifyingProvider<TResult> provider) {
            return new NotifyingProperty<TResult>(provider);
        }
    }
}