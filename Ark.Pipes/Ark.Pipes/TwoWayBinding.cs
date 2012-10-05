using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ark.Pipes {

    //OneTime: Target.Property = Source.Property.Value
    //OneWay: Target.Property = Source.Property
    //OneWayToSource: Source.Property = Target.Property
    //TwoWay: new TwoWayBinding(Source.Property, Target.Property)

    //Fix notification unsubscribing and binding removal.

    public static class TwoWayBinding {
        public static IDisposable Create<T>(Property<T> source, Property<T> target) {
            return new TwoWayBinding<T>(source, target);
        }

        public static IDisposable Create<TSource, TTarget>(Property<TSource> source, Property<TTarget> target, Func<TSource, TTarget> sourceToTarget, Func<TTarget, TSource> targetToSource) {
            return new TwoWayBinding<TSource, TTarget>(source, target, sourceToTarget, targetToSource);
        }
    }

    abstract class TwoWayBindingBase<TSource, TTarget> : IDisposable {
        protected Property<TSource> _source;
        protected Property<TTarget> _target;

        public TwoWayBindingBase(Property<TSource> source, Property<TTarget> target) {
            _source = source;
            _target = target;
#if !NOTIFICATIONS_DISABLE
            _source.ProviderChanged += OnSourceProviderChanged;
            _target.ProviderChanged += OnTargetProviderChanged;
#endif
        }

        protected abstract void OnSourceProviderChanged();
        protected abstract void OnTargetProviderChanged();

        public void Dispose() {
            _source.ProviderChanged -= OnSourceProviderChanged;
            _target.ProviderChanged -= OnTargetProviderChanged;
            _source = null;
            _target = null;
        }
    }

    sealed class TwoWayBinding<T> : TwoWayBindingBase<T, T> {
        public TwoWayBinding(Property<T> source, Property<T> target)
            : base(source, target) {
                OnSourceProviderChanged();
        }

        protected override void OnSourceProviderChanged() {
            _target.Provider = _source.Provider;
        }

        protected override void OnTargetProviderChanged() {
            _source.Provider = _target.Provider;
        }
    }

    sealed class TwoWayBinding<TSource, TTarget> : TwoWayBindingBase<TSource, TTarget> {
        Provider<TSource> _lastSource;
        Provider<TTarget> _lastTarget;
        Func<TSource, TTarget> _sourceToTarget;
        Func<TTarget, TSource> _targetToSource;

        public TwoWayBinding(Property<TSource> source, Property<TTarget> target, Func<TSource, TTarget> sourceToTarget, Func<TTarget, TSource> targetToSource)
            : base(source, target) {
            _sourceToTarget = sourceToTarget;
            _targetToSource = targetToSource;

            OnSourceProviderChanged();
        }

        protected override void OnSourceProviderChanged() {
            if (_lastSource == null || _source.Provider != _lastSource) {
                _lastTarget = Provider.Create(_sourceToTarget, _source.Provider);
                _target.Provider = _lastTarget;
            }
        }

        protected override void OnTargetProviderChanged() {
            if (_lastTarget == null || _target.Provider != _lastTarget) {
                _lastSource = Provider.Create(_targetToSource, _target.Provider);
                _source.Provider = _lastSource;
            }
        }
    }
}
