using System;
using Ark.Pipes;
using Microsoft.Xna.Framework;

namespace Ark.Animation { //.Pipes.Xna {
    public class XnaTrigger : GameComponent, ITrigger {
        ManualTrigger _trigger = new ManualTrigger();

        public event Action Triggered {
            add { _trigger.Triggered += value; }
            remove { _trigger.Triggered -= value; }
        }

        public XnaTrigger(Game game)
            : base(game) {
        }

        public override void Update(GameTime gameTime) {
            _trigger.Trigger();
        }
    }
}
