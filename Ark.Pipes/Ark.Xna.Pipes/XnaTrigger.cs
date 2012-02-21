using System;
using Ark.Pipes;
using Microsoft.Xna.Framework;

namespace Ark.Animation.Pipes.Xna {
    public class XnaTrigger : GameComponent, ITrigger {
        public event Action Triggered;

        public XnaTrigger(Game game) : base(game) { 
        }

        public override void Update(GameTime gameTime) {
            var handler = Triggered;
            if (handler != null) {
                handler();
            }
        }
    }
}
