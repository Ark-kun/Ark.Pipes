using System;
using Ark.Pipes;
using Ark.Geometry;
using Ark.Geometry.Primitives.Double;

namespace Ark.Pipes.Testing {
    using T = System.Int16;
    public class Tests {
        public static void Main() {

            var rndDouble = new RandomDouble();
            var v1 = rndDouble.RandomValue.Value;
            double v2 = rndDouble.RandomValue;

            var rndInt = (Provider<int>)(() => (int)(1000 * rndDouble.RandomValue));
            //Provider<int> timestamp = F
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
            var v2d1 = new Vector2Components(7, 13);
            //v2d1.Length.ValueChanged += () => { rndInt = 666; };
            //double l1 = v2d1.Length;
            //v2d1.X = rndDouble.RandomValue;
            //double l2 = v2d1.Length;

            v2d1.X = new Function<double>(() => 1);

            HelperProperties.Name[v2d1] = "v1";

            //Caching
            Action act = null;
            Property<double> p1 = rndDouble.RandomValue;
            Property<double> p2 = p1.AddChangeTrigger((callback) => act += callback);
            double v11 = p1;
            double v12 = p1;
            double v21 = p2;
            double v22 = p2;
            act();
            double v23 = p2;


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

        static void xxx() {
Provider<int> a1 = 1; //a = 1
int b1 = a1; //b = 1
Provider<int> c1 = a1.Value + b1; //c1 = 2
Provider<int> c2 = b1 + a1; //c1 = 2

var obj = new Tests();
int c3 = 1;
Provider<int> d = obj.Property1; //d === obj.Property1
obj.Property1 = c3; //d != obj.Property1
//Provider<int> d1 = () => 42;
Provider<int> d2 = (Func<int>)(() => 42);
var d3 = new Function<int>(() => 42);
var d4 = Provider<int>.Create(() => 42);
var start = DateTime.UtcNow;
var timer = Provider<double>.Create(() => (DateTime.UtcNow - start).TotalMilliseconds);
double elapsed = timer;
var point1 = Provider<Vector2>.Create(() => new Vector2(1, 2) * timer); //заметь, что мы умножаем на таймер, несмотря на то, что он является числом, а не провайдером
var point2 = Provider<Vector2>.Create((t) => new Vector2(1, 2) * t, timer);
Property<double> radius = 10; //учимся делать все переменные, которые могут меняться свойствами (если бы мы сделали raduis провайдером, то не смогли бы его заменять)
Property<Vector2> center = new Vector2(100, 200);// = new Ark.Input.XnaMouse().Position;
var point3 = Provider<Vector2>.Create((t, c, r) => c + new Vector2(Math.Cos(t), Math.Sin(t)) * r, timer, center, radius);
//center = new Ark.Input.XnaMouse().Position;
radius = Provider<double>.Create((t) => Math.Cos(t) * 10, timer);


        }

        int _prop0;
        public int Property0 {
            get { return _prop0; }
            set { _prop1 = value; }
        }
        public int Property01 { get; set; }
        //Provider<int>
        Provider<int> _prop1;
        public Provider<int> Property11 { get; set; }
        public Provider<int> Property1 {
            get { return _prop1; }
            set { _prop1 = value; }
        }

        Property<int> _prop2;
        public Property<int> Property2 {
            get { return _prop2; }
            set { _prop2.Provider = value; }
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