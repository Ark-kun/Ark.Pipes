using System;
using Ark.Abstract;
using Ark.Animation;
using Ark.Geometry;
using Ark.Geometry.Curves;
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

namespace Ark.Geometry.Curves {
    public class HomingVelocityBullet {
        public static Vector2 Velocity(Vector2 position, Vector2 target, TFloat speed) {
            Vector2 delta = target - position;
            if (delta.IsZero()) {
                return default(Vector2);
            }
            delta.Normalize();
            Vector2 velocity = delta * speed;
            return velocity;
        }
    }

    public class HomingAccelerationBullet {
        public static PositionWithVelocity2 PositionAndVelocity(PositionWithVelocity2 state, Vector2 target, TFloat attraction) {
            Vector2 position = state.Position;
            Vector2 velocity = state.Velocity;

            Vector2 direction = target - position;
            if (direction.IsZero()) {
                return default(PositionWithVelocity2);
            }
            direction.Normalize();

            PositionWithVelocity2 d;
            d.Position = velocity;
            d.Velocity = direction * attraction;
            return d;
        }

        //Orientation is buggy
        public static OrientedPosition2WithVelocities OrientedPositionWithVelocities(OrientedPosition2WithVelocities state, Vector2 target, TFloat attraction) {
            Vector2 position = state.Value.Position;
            Vector2 velocity = state.D.Position;

            Vector2 direction = velocity;
            Vector2 targetDirection = target - position;
            if (targetDirection.IsZero()) {
                return default(OrientedPosition2WithVelocities);
            }
            targetDirection.Normalize();
            Vector2 force = targetDirection * attraction;
            if (!direction.IsZero()) {
                direction.Normalize();
            }
            TFloat cross = -Vector3.Cross(force.ToVector3(), direction.ToVector3()).Z;
            return new OrientedPosition2WithVelocities(state.D, new OrientedPosition2(force, cross));
        }
    }
}