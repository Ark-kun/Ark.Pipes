using System;
using System.Collections.Generic;
using System.Threading;

namespace Ark.Pipes {
    public static partial class Extensions {
#if false
        public static IEnumerable<T> AsEnumerable<T>(this IOut<T> provider) {
            while (true) {
                yield return provider.GetValue();
            }
        }

        public IEnumerable<T> AsEnumerableChanges<T>(this INotifyingOut<T> provider) {
            var trigger = new ManualResetEvent(false);
            Action onChanged = () => trigger.Set();
            provider.Notifier.ValueChanged += onChanged;
            while (true) {
                yield return provider.GetValue();
                if (provider.Notifier.IsReliable) {
                    trigger.Reset();
                }
            }
            provider.Notifier.ValueChanged -= onChanged;
        }
#endif


#if !NOTIFICATIONS_DISABLE
        public static Provider<T> WithChangeTrigger<T>(this IOut<T> provider, ITrigger changedTrigger) {
            return Source<T>.Create(provider, changedTrigger);
        }
#endif
    }
}
