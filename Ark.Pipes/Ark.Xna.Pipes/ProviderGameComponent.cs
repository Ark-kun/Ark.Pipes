using System;
using Ark.Pipes;
using Microsoft.Xna.Framework;

namespace Ark.Xna.Pipes {
    public abstract class ProviderGameComponent<T> : Provider<T>, IUpdateable, IGameComponent /*, IDisposable*/ {
        // Fields
        private bool _enabled = true;
        //private Game _game;
        private int _updateOrder;

        // Events
        //public event EventHandler Disposed;
#if XNA3
        public event EventHandler EnabledChanged;
        public event EventHandler UpdateOrderChanged;
#else
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
#endif
        //// Methods
        //public ProviderGameComponent(Game game) {
        //    _game = game;
        //}

        //public void Dispose() {
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing) {
        //    if (disposing) {
        //        lock (this) {
        //            if (Game != null) {
        //                Game.Components.Remove(this);
        //            }
        //            if (Disposed != null) {
        //                Disposed(this, EventArgs.Empty);
        //            }
        //        }
        //    }
        //}

        //~ProviderGameComponent() {
        //    Dispose(false);
        //}

        public virtual void Initialize() {
        }

        protected virtual void OnEnabledChanged(object sender, EventArgs args) {
            if (EnabledChanged != null) {
                EnabledChanged(this, args);
            }
        }

        protected virtual void OnUpdateOrderChanged(object sender, EventArgs args) {
            if (UpdateOrderChanged != null) {
                UpdateOrderChanged(this, args);
            }
        }

        public virtual void Update(GameTime gameTime) {
        }

        // Properties
        public bool Enabled {
            get {
                return _enabled;
            }
            set {
                if (_enabled != value) {
                    _enabled = value;
                    OnEnabledChanged(this, EventArgs.Empty);
                }
            }
        }

        //public Game Game {
        //    get {
        //        return _game;
        //    }
        //}

        public int UpdateOrder {
            get {
                return _updateOrder;
            }
            set {
                if (_updateOrder != value) {
                    _updateOrder = value;
                    OnUpdateOrderChanged(this, EventArgs.Empty);
                }
            }
        }
    }
}
