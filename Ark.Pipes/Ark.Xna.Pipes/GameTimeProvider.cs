using Ark.Xna.Pipes;
using Microsoft.Xna.Framework;

namespace Ark.Animation.Pipes.Xna {
    public class GameTimeProvider : ProviderGameComponent<float> {
        float _time;

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            _time = (float)gameTime.TotalGameTime.TotalMilliseconds;
        }

        public override float GetValue() {
            return _time;
        }
    }
}
