using System;
using Ark.Pipes.Animation;
using Ark.Borrowed.Net.Microsoft.Xna.Framework;

namespace Ark.Pipes.Physics {
    public class ForcesDrivenMaterialPoint : MaterialPoint {
        Clock _clock;
        DateTime _lastTick = DateTime.MinValue;

        public ForcesDrivenMaterialPoint(Clock clock, double mass, Vector3 position, Vector3 velocity = new Vector3())
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
            double t = time.TotalSeconds;
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
