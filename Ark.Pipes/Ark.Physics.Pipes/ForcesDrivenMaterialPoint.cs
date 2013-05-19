using System;
using Ark.Pipes;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

#if FRAMEWORK_ARK && FLOAT_TYPE_DOUBLE
using Ark.Geometry.Primitives.Double;
#elif FRAMEWORK_ARK && FLOAT_TYPE_SINGLE
using Ark.Geometry.Primitives.Single;
#elif FRAMEWORK_XNA && FLOAT_TYPE_SINGLE
using Microsoft.Xna.Framework;
#elif FRAMEWORK_WPF && FLOAT_TYPE_DOUBLE
using System.Windows;
using System.Windows.Media.Media3D;
using Vector2 = System.Windows.Vector;
using Vector3 = System.Windows.Media.Media3D.Vector3D;
#else
#error Bad geometry framework
#endif

namespace Ark.Physics { //.Pipes {
    public class ForcesDrivenMaterialPoint : MaterialPoint {
        ITrigger _trigger;
        DateTime _lastTick = DateTime.MinValue;

        public ForcesDrivenMaterialPoint(ITrigger trigger, TFloat mass, Vector3 position, Vector3 velocity = new Vector3())
            : base(mass, position, velocity) {
            _trigger = trigger;
            _trigger.Triggered += HandleTick;
        }

        void HandleTick() {
            var newTick = DateTime.UtcNow;
            if (_lastTick != DateTime.MinValue) {
                Update(newTick - _lastTick);
            }
            _lastTick = newTick;
        }

        void Update(TimeSpan time) {
            TFloat t = (TFloat)time.TotalSeconds;
            Vector3 combinedForce = new Vector3();
            foreach (var force in _forces) {
                combinedForce += force;
            }
            Vector3 acceleraton = combinedForce / _mass;
            _acceleraton.Value = acceleraton;
            _velocity.Value += _acceleraton.Value * t;
            _position.Value += _velocity.Value * t;
        }
    }
}
