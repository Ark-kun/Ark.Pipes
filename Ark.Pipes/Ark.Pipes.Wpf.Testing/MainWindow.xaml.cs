using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using Ark.Borrowed.Net.Microsoft.Xna.Framework;
using Ark.Pipes.Animation;
using Ark.Pipes.Input;
using Ark.Pipes.Physics;
using Ark.Pipes.Physics.Forces;
using Ark.Pipes.Wpf;
using System.Windows.Shapes;

namespace Ark.Pipes.Wpf.Testing {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        NotifyingProperty<float> _prop = 10;
        DependencyPropertyAdapter<float> _adapter;
        DC _dc = new DC();

        public DependencyPropertyAdapter<float> Adapter {
            get { return _adapter; }
        }

        public MainWindow() {
            InitializeComponent();
            _adapter = new DependencyPropertyAdapter<float>(_prop);
            //RegisterName("adapter", _adapter);
            _dc.Adapter = _adapter;
            _dc.TinyAdapter = new NotifyPropertyChangedAdapter<float>(_prop);
            MyGrid.DataContext = _dc;
            //Binding myBinding = new Binding("Value");
            //myBinding.Source = _adapter;
            //rectangle1.SetBinding(Rectangle.WidthProperty, myBinding);

            //const double invScale = 4000;
            const double invScale = 10;
            const double scale = 1.0 / invScale;
            
            var mouse = new WpfMouse(MyGrid);
            var clock = new WpfClock();
            var mouseComponents = mouse.Position.ToVector2Components();
            //var mouseComponents = new Vector2Components(mouse.Position);
            //var mouseComponents =  Vector2Components.From<Point>(mouse.Position);
            //var invertedY = new Function<double, double>((y) => MyGrid.ActualHeight - y, components.Y);
            var modifiedX = new Function<double, double>((x) => x - cursor.Width * 0.5, mouseComponents.X);
            var modifiedY = new Function<double, double>((y) => y - cursor.Height * 0.5, mouseComponents.Y);
            var adaptedX = new ManualUpdateAdapter<double>(modifiedX, clock);
            //var adaptedY = new ManualUpdateAdapter<double>(invertedY, clock);
            var adaptedY = new ManualUpdateAdapter<double>(modifiedY, clock);

            cursor.SetBinding(Canvas.LeftProperty, new Binding("Value") { Source = adaptedX, Mode = BindingMode.OneWay });
            cursor.SetBinding(Canvas.TopProperty, new Binding("Value") { Source = adaptedY, Mode = BindingMode.OneWay });


            var mouse3d = new Function<Point, Vector3>((p) => new Vector3(p.X * scale, 0, (MyGrid.ActualHeight - p.Y) * scale), mouse.Position);
            var ball = new ForcesDrivenMaterialPoint(clock, 1.0, new Vector3(((double)Ball.GetValue(Canvas.LeftProperty) - Ball.Height * 0.5) * scale, 0, (MyGrid.ActualHeight - (double)Ball.GetValue(Canvas.TopProperty) - Ball.Height * 0.5)) * scale);
            var attraction = new PointAttractionForce(mouse3d, 10);
            ball.Forces.Add(attraction.GetForceOnObject(ball));

            var ballPositionComponents = new Vector3Components(ball.Position);
            var modifiedBallX = new Function<double, double>((x) => x * invScale - Ball.Width * 0.5, ballPositionComponents.X);
            var modifiedBallY = new Function<double, double>((z) => MyGrid.ActualHeight - z * invScale - Ball.Height * 0.5, ballPositionComponents.Z);
            var adaptedBallX = new ManualUpdateAdapter<double>(modifiedBallX, clock);
            var adaptedBallY = new ManualUpdateAdapter<double>(modifiedBallY, clock);

            Ball.SetBinding(Canvas.LeftProperty, new Binding("Value") { Source = adaptedBallX, Mode = BindingMode.OneWay });
            Ball.SetBinding(Canvas.TopProperty, new Binding("Value") { Source = adaptedBallY, Mode = BindingMode.OneWay });


            var ball2 = new ForcesDrivenMaterialPoint(clock, 1.0, new Vector3(((double)Ball2.GetValue(Canvas.LeftProperty) - Ball2.Height * 0.5) * scale, 0, (MyGrid.ActualHeight - (double)Ball2.GetValue(Canvas.TopProperty) - Ball2.Height * 0.5) * scale));
            var spring = new ElasticForce(4.0, ball, ball2);            
            ball.Forces.Add(spring.ForceOnObject1);
            ball2.Forces.Add(spring.ForceOnObject2);

            var gravity = new EarthGravityForce();
            ball2.Forces.Add(gravity.GetForceOnObject(ball2));
            ball.Forces.Add(gravity.GetForceOnObject(ball));

            var friction1 = new ViscousFrictionForce(4);
            var friction2 = new ConstantFrictionForce(2);
            ball2.Forces.Add(friction2.GetForceOnObject(ball2));

            var ball2PositionComponents = new Vector3Components(ball2.Position);
            var modifiedBall2X = new Function<double, double>((x) => x * invScale - Ball2.Width * 0.5, ball2PositionComponents.X);
            var modifiedBall2Y = new Function<double, double>((z) => MyGrid.ActualHeight - z * invScale - Ball2.Height * 0.5, ball2PositionComponents.Z);
            var adaptedBall2X = new ManualUpdateAdapter<double>(modifiedBall2X, clock);
            var adaptedBall2Y = new ManualUpdateAdapter<double>(modifiedBall2Y, clock);

            Ball2.SetBinding(Canvas.LeftProperty, new Binding("Value") { Source = adaptedBall2X, Mode = BindingMode.OneWay });
            Ball2.SetBinding(Canvas.TopProperty, new Binding("Value") { Source = adaptedBall2Y, Mode = BindingMode.OneWay });

            
            var modifiedSpring = new Function<double, double>((f) => 1 + 20 * 1.0 / (1.0 + Math.Exp(-f * -0.1 )), spring);
            var adaptedSpring = new ManualUpdateAdapter<double>(modifiedSpring, clock);
            SpringLine.SetBinding(Line.StrokeThicknessProperty, new Binding("Value") { Source = adaptedSpring, Mode = BindingMode.OneWay });
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            _prop.Provider = new Function<float, float>((s) => (float)(100 + Math.Sin(s) * 100), new MyTimer(Dispatcher));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e) {

        }


    }

    public class ManualUpdateAdapter<T> : Provider<T>, INotifyPropertyChanged {
        IOut<T> _source;
        Clock _updateClock;

        public event PropertyChangedEventHandler PropertyChanged;

        public ManualUpdateAdapter(IOut<T> source, Clock updateClock) {
            _source = source;
            _updateClock = updateClock;
            _updateClock.Tick += OnPropertyChanged;
        }

        public override T GetValue() {
            return _source.GetValue();
        }

        protected void OnPropertyChanged() {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs("Value"));
            }
        }
    }

    public class MyTimer : Provider<float> {
        System.Timers.Timer _t1 = new System.Timers.Timer(1000 / 100);
        DateTime _firstTime;
        DateTime _lastTime;
        Dispatcher _disp;

        public MyTimer(Dispatcher disp) {
            _disp = disp;
            _firstTime = DateTime.Now;
            _t1.Elapsed += Callback;
            _t1.Start();
        }

        public override float GetValue() {
            return (float)(_lastTime - _firstTime).TotalSeconds;
        }

        void Callback(object sender, System.Timers.ElapsedEventArgs e) {
            _lastTime = e.SignalTime;
            _disp.Invoke((Action)OnValueChanged);
        }
    }

    public class DC {
        public DependencyPropertyAdapter<float> Adapter { get; set; }
        public NotifyPropertyChangedAdapter<float> TinyAdapter { get; set; }
    }


}
