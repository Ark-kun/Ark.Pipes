using System;
using System.Collections.Generic;
using System.Threading;

namespace Ark.Pipes {
    public static class Extensions {
        public static IEnumerable<T> AsEnumerable<T>(this IOut<T> provider) {
            while (true) {
                yield return provider.GetValue();
            }
        }

#if false
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

#if !PORTABLE
        public static IObservable<T> AsObservable<T>(this INotifyingOut<T> provider) {
            return new ObservableProvider<T>(provider);
        }
#endif
    }
}
