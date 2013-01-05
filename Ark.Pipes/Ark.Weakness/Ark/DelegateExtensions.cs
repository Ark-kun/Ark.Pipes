using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Ark {
    static class DelegateExtensions {
        static int GetGoodHashCodeInternal(this Delegate del) {
            return del.Method.GetHashCode() ^ RuntimeHelpers.GetHashCode(del.Target);
        }

        public static int GetGoodHashCode(this Delegate del) {
            var multicastDelegate = del as MulticastDelegate;
            if (multicastDelegate != null) {
                return GetGoodHashCode(multicastDelegate);
            }
            return GetGoodHashCodeInternal(del);
        }

        public static int GetGoodHashCode(this MulticastDelegate multicastDel) {
            var delegates = multicastDel.GetInvocationList();
            if (delegates == null || delegates.Length == 0) {
                return multicastDel.GetType().GetHashCode();
            } else {
                int hash = 0;
                foreach (var del in multicastDel.GetInvocationList()) {
                    hash = hash * 0x21 + GetGoodHashCodeInternal(del);
                }
                return hash;
            }
        }

        public static IEnumerable<TDelegate> GetTypedInvocationList<TDelegate>(this TDelegate handler) where TDelegate : class {
            if (handler == null) { 
                return new TDelegate[0];
            }
            var del = handler as Delegate;
            if (del == null) {
                throw new ArgumentException("The TDelegate generic parameter must be a delegate type.");
            }
            var delegates = del.GetInvocationList();

            return delegates.OfType<TDelegate>();
        }
    }
}
