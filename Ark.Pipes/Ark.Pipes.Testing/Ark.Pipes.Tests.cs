using Microsoft.Xna.Framework;
using System;

namespace Ark.Pipes {
    public class Tests {
        public static void Main() {

            var rndDouble = new RandomDouble();
            var v1 = rndDouble.RandomValue.Value;
            double v2 = rndDouble.RandomValue;

            var rndInt = (Provider<int>)(() => (int)(1000 * rndDouble.RandomValue));
            var v3 = rndInt.Value;
            int v4 = rndInt;
            rndInt = 666;
            int v42 = rndInt;

            var rndVector2 = (Provider<Vector2>)(() => new Vector2(rndInt, rndInt));
            var v5 = rndVector2.Value;
            Vector2 v6 = rndVector2;

            var rndVector3 = (Provider<Vector3>)(() => new Vector3(rndInt, rndInt, 13));
            var v7 = rndVector3.Value;
            Vector3 v8 = rndVector3;

            //var rndDoubleScaled = (Provider<double>)((k) => k * rndDouble.RandomValue);
            //var v1 = rndDouble.RandomValue.Value;
            //double v2 = rndDouble.RandomValue;
            rndDouble = rndDouble;
        }
    }

    public class RandomDouble {
        Random _rnd = new Random();
        Provider<double> _rv;

        public RandomDouble() {
            _rv = (Func<double>)(() => _rnd.NextDouble());
        }

        public Provider<double> RandomValue {
            get {
                return _rv;
            }
        }
    }

    public class Mouse {
        static Provider<Vector2> _p = (Func<Vector2>)(() => {
            var mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        });

        public static Provider<Vector2> Position {
            get {
                return _p;
            }
        }
    }

    public class MousePosition : Provider<Vector2> {
        public override Vector2 Value {
            get {
                var mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
                return new Vector2(mouseState.X, mouseState.Y);
            }
        }
    }
}