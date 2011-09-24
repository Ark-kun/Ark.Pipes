
namespace Ark.Pipes.Physics.Forces {
    public class ElasticForce : TwoBodyForce {
        double _springConstant;
        double _length;

        public ElasticForce(double elasticity, MaterialPoint obj1, MaterialPoint obj2)
            : this(elasticity, obj1, obj2, (obj1.Position.Value - obj2.Position.Value).Length) {
        }

        public ElasticForce(double springConstant, MaterialPoint obj1, MaterialPoint obj2, double length)
            : base(obj1, obj2) {
            _springConstant = springConstant;
            _length = length;
        }

        protected override double GetMagnitude() {
            return _springConstant * ((_obj1.Position.Value - _obj2.Position.Value).Length - _length);
        }
    }
}
