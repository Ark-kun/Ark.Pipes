using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test.XNAWindowsGame
{
    public class Sprite : IGameElement
    {
        #region IGameElement Members

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUpdatable Members

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
