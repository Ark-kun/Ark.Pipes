using System;
using Ark.Pipes.Animation;

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

namespace Ark.Pipes.Physics {
    public class ForcesDrivenMaterialPoint : MaterialPoint {
        Clock _clock;
        DateTime _lastTick = DateTime.MinValue;

        public ForcesDrivenMaterialPoint(Clock clock, TFloat mass, Vector3 position, Vector3 velocity = new Vector3())
            : base(mass, position, velocity) {
            _clock = clock;
            _clock.Tick += HandleTick;
        }

        void HandleTick() {
            var newTick = DateTime.Now;
            if (_lastTick != DateTime.MinValue) {
                Update(newTick - _lastTick);
            }
            _lastTick = newTick;
        }

        void Update(TimeSpan time) {
            TFloat t = (TFloat)time.TotalSeconds;
            Vector3 acceleraton = new Vector3();
            foreach (var force in _forces) {
                acceleraton += force;
            }
            _acceleraton.Value = acceleraton;
            _velocity.Value += _acceleraton.Value * t;
            _position.Value += _velocity.Value * t;
        }
    }
}
