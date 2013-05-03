using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ark {

    static class GCMonitor {
        //We don't use GC.CollectionCount because it's not available in Silverlight and requires full trust.
        static WeakReference _gcMonitor = new WeakReference(new object());
        static int _version = int.MaxValue;

        public static int Version { get { return _version; } }

        public static Func<bool> CreateGCChecker() {
            int version = _version;
            return () => { return CheckGCPassed(ref version); };
        }

        public static bool CheckGCPassed(ref int version) {
            if (!_gcMonitor.IsAlive) {
                _gcMonitor = new WeakReference(new object());
                _version++; //overflows are fine
            }

            if (version == _version) {
                return false;
            } else {
                version = _version;
                return true;
            }
        }
    }

    static class NotifyingGCMonitor {
        //We don't use GC.CollectionCount because it's not available in Silverlight and requires full trust.
        static WeakReference _gcMonitor = new WeakReference(new object());
        static int _version = int.MaxValue;
        static Action _garbageCollectedHandlers;

        public static int Version { get { return _version; } }

        public static void CheckGC() {
            if (!_gcMonitor.IsAlive) {
                _gcMonitor = new WeakReference(new object());
                _version++; //overflows are fine
                SignalGarbageCollected();
            }
        }

        static void SignalGarbageCollected() {
            var handlers = _garbageCollectedHandlers;
            if (handlers != null) {
                handlers();
            }
        }

        public static event Action GarbageCollected {
            add { _garbageCollectedHandlers += value; }
            remove { _garbageCollectedHandlers -= value; }
        }
    }
}
