using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ark.Pipes;
using Microsoft.Xna.Framework;

namespace Ark.XNA {
    public class Time : ProviderGameComponent<float> {
        float _time;

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            _time = (float)gameTime.TotalGameTime.TotalMilliseconds;
        }

        public override float Value {
            get {
                return _time;
            }
        }
    }
}
