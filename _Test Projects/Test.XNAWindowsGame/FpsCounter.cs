using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FpsCounter : DrawableGameComponent {
    SharedSpriteBatch spriteBatch;
    Color fpsColor = Color.BlanchedAlmond;
    SpriteFont someFont;
    Game game;
    Vector2 position;
    int frameCount = 0;

    //public FpsCounter(Game game)
    //    : base(game) {
    //    this.game = game;
    //}
    public FpsCounter(Game game, SharedSpriteBatch spriteBatch, Vector2 position)
        : base(game) {
        this.game = game;
        this.spriteBatch = spriteBatch;
        this.position = position;
        
    }

    protected override void LoadContent() {
        base.LoadContent();
        someFont = game.Content.Load<SpriteFont>("Some Font");
    }
    public override void Update(GameTime gameTime) {
        base.Update(gameTime);

    }
    public override void Draw(GameTime gameTime) {
        base.Draw(gameTime);

        frameCount += 1;
        var FPS = frameCount / gameTime.TotalRealTime.TotalSeconds;

        spriteBatch.SharedBegin();
        spriteBatch.DrawString(someFont, FPS.ToString(), position, fpsColor);
        //spriteBatch.DrawString(someFont, gameTime.TotalGameTime.TotalSeconds.ToString(), fpsPosition, fpsColor);
        spriteBatch.SharedEnd();
    }
}

