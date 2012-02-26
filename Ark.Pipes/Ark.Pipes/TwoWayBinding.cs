using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ark.Pipes {

    //OneTime: Target.Property = Source.Property.Value
    //OneWay: Target.Property = Source.Property
    //OneWayToSource: Source.Property = Target.Property
    //TwoWay: new TwoWayBinding(Source.Property, Target.Property)

    public abstract class TwoWayBindingBase<TSource, TTarget> {
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
    }

    public class TwoWayBinding<T> : TwoWayBindingBase<T, T> {
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

    public class TwoWayBinding<TSource, TTarget> : TwoWayBindingBase<TSource, TTarget> {
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
                _lastTarget = new Function<TSource, TTarget>(_sourceToTarget, _source.Provider);
                _target.Provider = _lastTarget;
            }
        }

        protected override void OnTargetProviderChanged() {
            if (_lastTarget == null || _target.Provider != _lastTarget) {
                _lastSource = new Function<TTarget, TSource>(_targetToSource, _target.Provider);
                _source.Provider = _lastSource;
            }
        }
    }
}
