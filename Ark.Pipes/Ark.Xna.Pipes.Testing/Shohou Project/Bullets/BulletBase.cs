using System.Collections.Generic;
using System.Linq;
using Ark.Animation;
using Ark.Animation.Bullets;
using Ark.Geometry.Transforms;
using Ark.Pipes;
using Microsoft.Xna.Framework;

namespace Ark.Animation.Bullets { //.Pipes.Xna {
    public class BulletBase<T> : DrawableGameComponent, IBullet<T> {
        private IBulletFactory<T> _parent;

        public BulletBase(Game game, IBulletFactory<T> parent)
            : base(game) {
            _parent = parent;
        }

        Property<T> _position = new Property<T>();
        public Provider<T> Position {
            get {
                return _position;
            }
            set {
                _position.Provider = value;
            }
        }

        public IBulletFactory<T> Parent {
            get {
                return _parent;
            }
        }
    }

    public class BulletFactoryBase<T> : DrawableGameComponent, IBulletFactory<T> {
        private ITransform<T> _transform;
        private HashSet<IBullet<T>> _bullets = new HashSet<IBullet<T>>();
        private HashSet<IBulletFactory<T>> _bulletFactories = new HashSet<IBulletFactory<T>>();

        public BulletFactoryBase(Game game, ITransform<T> transform)
            : base(game) {
            _transform = transform;
        }

        IEnumerable<IBulletFactory<T>> IContainer<IBulletFactory<T>>.Elements {
            get {
                return _bulletFactories;
            }
        }

        public IEnumerable<IBullet<T>> Elements {
            get {
                return _bullets;
            }
        }

        public ITransform<T> Transform {
            get {
                return _transform;
            }
        }

        protected IEnumerable<IBullet<T>> Bullets {
            get {
                return _bullets;
            }
        }

        protected IEnumerable<IBulletFactory<T>> BulletFactories {
            get {
                return _bulletFactories;
            }
        }
    }

    public class BulletFactoryBulletBase<T> : DrawableGameComponent, IBulletFactoryBullet<T> {
        private IBulletFactory<T> _parent;
        private ITransform<T> _transform;
        private HashSet<IBulletFactoryBullet<T>> _bulletFactories = new HashSet<IBulletFactoryBullet<T>>();

        public BulletFactoryBulletBase(Game game, ITransform<T> transform, IBulletFactory<T> parent)
            : base(game) {
            _parent = parent;
            _transform = transform;
        }

        Provider<T> _position = Constant<T>.Default;
        public Provider<T> Position {
            get {
                return _position;
            }
            set {
                _position = value;
            }
        }

        public IBulletFactory<T> Parent {
            get {
                return _parent;
            }
        }

        IEnumerable<IBulletFactory<T>> IContainer<IBulletFactory<T>>.Elements {
            get {
                return _bulletFactories.OfType<IBulletFactory<T>>();
            }
        }

        public IEnumerable<IBullet<T>> Elements {
            get {
                return _bulletFactories.OfType<IBullet<T>>();
            }
        }

        protected HashSet<IBulletFactoryBullet<T>> BulletFactories {
            get {
                return _bulletFactories;
            }
        }

        public ITransform<T> Transform {
            get {
                return _transform;
            }
        }
    }
}
