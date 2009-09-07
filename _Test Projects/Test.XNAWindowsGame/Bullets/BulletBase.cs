using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Ark.XNA.Bullets {
    public class BulletBase<T> : DrawableGameComponent, IBullet<T> {
        private T _position;
        private IBulletFactory<T> _parent;

        public BulletBase(Game game, IBulletFactory<T> parent)
            : base(game) {
            _parent = parent;
        }

        public T Position {
            get {
                return _position;
            }
            protected set {
                _position = value;
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
        private List<IBullet<T>> _bullets = new List<IBullet<T>>();
        private List<IBulletFactory<T>> _bulletFactories = new List<IBulletFactory<T>>();

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

        protected IList<IBullet<T>> Bullets {
            get {
                return _bullets;
            }
        }

        protected IList<IBulletFactory<T>> BulletFactories {
            get {
                return _bulletFactories;
            }
        }
    }

    public class BulletFactoryBulletBase<T> : DrawableGameComponent, IBulletFactoryBullet<T> {
        private T _position;
        private IBulletFactory<T> _parent;
        private ITransform<T> _transform;
        private List<IBulletFactoryBullet<T>> _bulletFactories = new List<IBulletFactoryBullet<T>>();

        public BulletFactoryBulletBase(Game game, ITransform<T> transform, IBulletFactory<T> parent)
            : base(game) {
            _parent = parent;
            _transform = transform;
        }

        public T Position {
            get {
                return _position;
            }
            protected set {
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

        protected List<IBulletFactoryBullet<T>> BulletFactories {
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
