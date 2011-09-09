using Microsoft.Xna.Framework;
using System;

namespace Ark.Pipes.Testing {
    public class Tests {
        public static void Main() {

            var rndDouble = new RandomDouble();
            var v1 = rndDouble.RandomValue.Value;
            double v2 = rndDouble.RandomValue;

            var rndInt = (Provider<int>)(() => (int)(1000 * rndDouble.RandomValue));
            var v3 = rndInt.Value;
            int v4 = rndInt;
            //rndInt = 666;
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
            //rndDouble = rndDouble;

            //generic operators
            Func<int, string> ff1 = i => i.ToString();
            //Provider<string> p11 = (Provider<string>)ff1;
            Provider<string> p12 = Provider<string>.op_Implicit(ff1);

            Func<string, string> ff2 = s => s;
            Provider<string> p21 = (Provider<string>)ff2;
            Provider<string> p22 = Provider<string>.op_Implicit(ff2);

            //Vector2D
            var v2d1 = new Vector2D(7, 13);
            v2d1.Length.Changed += () => { rndInt = 666; };
            double l1 = v2d1.Length;
            v2d1.X = rndDouble.RandomValue;
            double l2 = v2d1.Length;

            v2d1.X = new Function<double>(() => 1);

            //ReadOnlyProperty
            //ReadOnlyProperty<int> rop = rndInt;
            //var rop = new ReadOnlyProperty<int>(rndInt);
            //int v9 = rop;
            ////rop = 134;
            //int v10 = rop;

            //Property<int> prop = rndInt;
            ////rop = prop;
            //prop = rop;
            
            //Interface conversion
            //Provider<int> pr2 = (Provider<int>)(IOut<int>)(new Const<int>(13));
            //IOut<int> iout1 = 13;
            //ReadOnlyProperty<int> rop2 = (IOut<int>)null;
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
}