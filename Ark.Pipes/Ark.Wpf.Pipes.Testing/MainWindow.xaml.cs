using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Ark.Animation;
using Ark.Geometry;
using Ark.Geometry.Primitives.Double;
using Ark.Input;
using Ark.Physics;
using Ark.Physics.Forces;
using Ark.Pipes;
using Ark.Wpf;

namespace Ark.Wpf.Pipes.Testing {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        Property<float> _prop = 10;
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
            _dc.TinyAdapter = _prop;
            MyGrid.DataContext = _dc;
            //Binding myBinding = new Binding("Value");
            //myBinding.Source = _adapter;
            //rectangle1.SetBinding(Rectangle.WidthProperty, myBinding);

            //const double invScale = 4000;
            const double invScale = 10;
            const double scale = 1.0 / invScale;

            var mouse = new WpfMouse(MyGrid);
            var clock = new WpfTrigger();

            var mousePosition = mouse.Position.ToVectors2();            
            mousePosition = mousePosition.AddChangeTrigger(clock);
            var mouse3d = mousePosition.ToVectors3XZ().Multiply(scale);

            //Mouse "cursor"
            var cursorImageCenter = new Point(cursor.Width / 2, cursor.Height / 2).ToVector2();
            var cursorImagePosition = mousePosition.Subtract(cursorImageCenter);
            cursor.SetCanvasPosition(cursorImagePosition);

            //Ball1
            var ballImageCenter = new Point(Ball.Width / 2, Ball.Height / 2).ToVector2();            
            var ball1StartingPosition = ((Ball.GetCanvasPosition() - ballImageCenter) * scale).ToVector3XZ();
            var ball = new ForcesDrivenMaterialPoint(clock, 1.0, ball1StartingPosition);
            var ballImagePosition = ball.Position.ToVectors2XZ().Divide(scale).Subtract(ballImageCenter);
            Ball.SetCanvasPosition(ballImagePosition);

            //Ball2
            var ball2ImageCenter = new Point(Ball2.Width / 2, Ball2.Height / 2).ToVector2();
            var ball2StartingPosition = ((Ball2.GetCanvasPosition() - ball2ImageCenter) * scale).ToVector3XZ();
            var ball2 = new ForcesDrivenMaterialPoint(clock, 1.0, ball2StartingPosition);
            var ball2ImagePosition = ball2.Position.ToVectors2XZ().Divide(scale).Subtract(ballImageCenter);
            Ball2.SetCanvasPosition(ball2ImagePosition);

            //Forces
            var attraction = new PointAttractionForce(mouse3d, 10);
            var spring = new ElasticForce(4.0, ball, ball2);
            var gravity = new EarthGravityForce();
            var friction1 = new ViscousFrictionForce(4);
            var friction2 = new ConstantFrictionForce(2);

            ball.Forces.Add(attraction.GetForceOnObject(ball));
            ball.Forces.Add(spring.ForceOnObject1);
            ball.Forces.Add(gravity.GetForceOnObject(ball));

            ball2.Forces.Add(spring.ForceOnObject2);
            ball2.Forces.Add(gravity.GetForceOnObject(ball2));
            ball2.Forces.Add(friction2.GetForceOnObject(ball2));

            //Spring visualization
            var modifiedSpring = Provider.Create((f) => 1 + 20 * 1.0 / (1.0 + Math.Exp(-f * -0.1)), spring);
            SpringLine.SetBinding(Line.StrokeThicknessProperty, modifiedSpring);
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            _prop.Provider = Provider.Create((s) => (float)(100 + Math.Sin(s) * 100), new MyTimer(Dispatcher));
        }
    }

    public class ManualUpdateAdapter<T> : Provider<T>, INotifyPropertyChanged {
        IOut<T> _source;
        ITrigger _updateClock;

        public event PropertyChangedEventHandler PropertyChanged;

        public ManualUpdateAdapter(IOut<T> source, ITrigger updateClock) {
            _source = source;
            _updateClock = updateClock;
            _updateClock.Triggered += OnPropertyChanged;
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

    public class MyTimer : ProviderWithNotifier<float> {
        System.Timers.Timer _t1 = new System.Timers.Timer(1000 / 100);
        DateTime _firstTime;
        DateTime _lastTime;
        Dispatcher _dispatcher;

        public MyTimer(Dispatcher dispatcher) {
            _notifier.SetReliability(true);
            _dispatcher = dispatcher;
            _firstTime = DateTime.Now;
            _t1.Elapsed += Callback;
            _t1.Start();
        }

        public override float GetValue() {
            return (float)(_lastTime - _firstTime).TotalSeconds;
        }

        void Callback(object sender, System.Timers.ElapsedEventArgs e) {
            _lastTime = e.SignalTime;
            _dispatcher.Invoke((Action)_notifier.SignalValueChanged);
        }
    }

    public class DC {
        public DependencyPropertyAdapter<float> Adapter { get; set; }
        public Provider<float> TinyAdapter { get; set; }
    }


}
