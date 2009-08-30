using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class MouseControlledObject : GameComponent {
    Game _game;
    IHasChangeablePosition _object;

    public MouseControlledObject(Game game, IHasChangeablePosition obj)
        : base(game) {
        _game = game;
        _object = obj;
    }
    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
        var mouseState = Mouse.GetState();
        _object.Position = new Vector2() { X = mouseState.X, Y = mouseState.Y };
    }

}

