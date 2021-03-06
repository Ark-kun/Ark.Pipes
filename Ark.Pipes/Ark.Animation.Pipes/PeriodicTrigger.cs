﻿using System;
using Ark.Pipes;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

namespace Ark.Animation {
    //PeriodicTrigger precision is rough and not guaranteed
    public class PeriodicTrigger : TriggerBase
#if !NOTIFICATIONS_DISABLE
        , IValueChangeListener
#endif
    {
        TFloat _interval;
        TFloat _lastTime;
        Provider<TFloat> _timer;

#if !NOTIFICATIONS_DISABLE
        public PeriodicTrigger(TFloat interval, Provider<TFloat> timer) {
            _interval = interval;
            _timer = timer;
            _lastTime = _timer.Value;
            _timer.Notifier.AddListener(this);
        }
#endif
        public PeriodicTrigger(TFloat interval, Provider<TFloat> timer, ITrigger changeTrigger)
            : this(interval, 0, timer, changeTrigger) {
        }

        public PeriodicTrigger(TFloat interval, TFloat waitTime, Provider<TFloat> timer, ITrigger changeTrigger) {
            _interval = interval;
            _timer = timer;
            _lastTime = _timer.Value + waitTime;
            changeTrigger.Triggered += OnTick;
        }

        void OnTick() {
            TFloat time = _timer.Value;
            while (time - _lastTime > _interval) {
                SignalTriggered();
                _lastTime += _interval;
            }
        }

#if !NOTIFICATIONS_DISABLE
        void IValueChangeListener.OnValueChanged() {
            OnTick();
        }
#endif
    }
}
