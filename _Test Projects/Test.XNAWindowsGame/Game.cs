using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Test.XNAWindowsGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private const int TargetFrameRate = 60;
        private const int BackBufferWidth = 1280;
        private const int BackBufferHeight = 720;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont someFont;
        int frameCount = 0;

        Vector2 fpsPosition;
        Color fpsColor = Color.BlanchedAlmond;

        List<IGameElement> gameElements = new List<IGameElement>();

        public Game()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            someFont = Content.Load<SpriteFont>("Some Font");

            //spriteTexture = Content.Load<Texture2D>("6861023");
            //Texture2D spriteTexture1 = null;
            //Texture2D spriteTexture2 = Content.Load<Texture2D>("Circle5");
            Texture2D spriteTexture1 = Content.Load<Texture2D>("Circle2");
            Texture2D spriteTexture2 = Content.Load<Texture2D>("Circle3");
            //sprite = new RotatingSprite() { Sprite = spriteTexture1 };
            sprite = new CoolSprite() { Sprite1 = spriteTexture1, Sprite2 = spriteTexture2 };
            gameElements.Add(sprite);
        }

        protected override void Initialize()
        {
            fpsPosition = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);                     

            base.Initialize();
        }


        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            HandleInput();
            GetInput();

            foreach (var ge in gameElements) {
                ge.Update(gameTime);
            }

            base.Update(gameTime);
        }

        private void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            bool continuePressed = keyboardState.IsKeyDown(Keys.Space);
        }


        //RotatingSprite sprite;
        CoolSprite sprite;
        

        private void GetInput()
        {
            // Get input state.
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 spritePosition = sprite.Position;
            var mouseState = Mouse.GetState();

            if(mouseState.LeftButton == ButtonState.Pressed){
                spritePosition = new Vector2() { X = mouseState.X, Y = mouseState.Y };                
            }            


            // If any digital horizontal movement input is found, override the analog movement.
            if (keyboardState.IsKeyDown(Keys.Left) ||
                keyboardState.IsKeyDown(Keys.A))
            {
                spritePosition.X -= 10.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Right) ||
                     keyboardState.IsKeyDown(Keys.D))
            {
                spritePosition.X += 10.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Up) ||
                keyboardState.IsKeyDown(Keys.W))
            {
                spritePosition.Y -= 10.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Down) ||
                     keyboardState.IsKeyDown(Keys.S))
            {
                spritePosition.Y += 10.0f;
            }
            sprite.Position = spritePosition;
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            frameCount += 1;
            var FPS = frameCount / gameTime.TotalRealTime.TotalSeconds;

            spriteBatch.Begin();
            //spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.DrawString(someFont, FPS.ToString(), fpsPosition, fpsColor);
            //spriteBatch.DrawString(someFont, gameTime.TotalGameTime.TotalSeconds.ToString(), fpsPosition, fpsColor);

            foreach (var ge in gameElements)
            {
                ge.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
