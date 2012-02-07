using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ark.Pipes {

    //OneTime: Target.Property = Source.Property.Value
    //OneWay: Target.Property = Source.Property
    //OneWayToSource: Source.Property = Target.Property
    //TwoWay: new TwoWayBinding(Source.Property, Target.Property)

    public abstract class NotifyingTwoWayBindingBase<TSource, TTarget> {
        protected NotifyingProperty<TSource> _source;
        protected NotifyingProperty<TTarget> _target;

        public NotifyingTwoWayBindingBase(NotifyingProperty<TSource> source, NotifyingProperty<TTarget> target) {
            _source = source;
            _target = target;

            _source.ProviderChanged += OnSourceProviderChanged;
            _target.ProviderChanged += OnTargetProviderChanged;            
        }

        protected abstract void OnSourceProviderChanged();
        protected abstract void OnTargetProviderChanged();
    }

    public class NotifyingTwoWayBinding<T> : NotifyingTwoWayBindingBase<T, T> {
        public NotifyingTwoWayBinding(NotifyingProperty<T> source, NotifyingProperty<T> target)
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

    public class NotifyingTwoWayBinding<TSource, TTarget> : NotifyingTwoWayBindingBase<TSource, TTarget> {
        NotifyingProvider<TSource> _lastSource;
        NotifyingProvider<TTarget> _lastTarget;
        Func<TSource, TTarget> _sourceToTarget;
        Func<TTarget, TSource> _targetToSource;

        public NotifyingTwoWayBinding(NotifyingProperty<TSource> source, NotifyingProperty<TTarget> target, Func<TSource, TTarget> sourceToTarget, Func<TTarget, TSource> targetToSource)
            : base(source, target) {
            _sourceToTarget = sourceToTarget;
            _targetToSource = targetToSource;

            OnSourceProviderChanged();
        }

        protected override void OnSourceProviderChanged() {
            if (_lastSource == null || _source.Provider != _lastSource) {
                _lastTarget = new NotifyingFunction<TSource, TTarget>(_sourceToTarget, _source.Provider);
                _target.Provider = _lastTarget;
            }
        }

        protected override void OnTargetProviderChanged() {
            if (_lastTarget == null || _target.Provider != _lastTarget) {
                _lastSource = new NotifyingFunction<TTarget, TSource>(_targetToSource, _target.Provider);
                _source.Provider = _lastSource;
            }
        }
    }
}
