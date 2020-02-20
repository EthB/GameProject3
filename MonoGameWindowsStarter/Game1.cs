using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        List<Brick> bricks;
        Ball ball;
        SpriteSheet sheet;
        AxisList world;
        Random random;
        Texture2D balltexture;
    

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            bricks = new List<Brick>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            random = new Random();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            balltexture = Content.Load<Texture2D>("ball");
            var t = Content.Load<Texture2D>("spritesheet");
            sheet = new SpriteSheet(t, 32, 32);

            var playerFrames = from index in Enumerable.Range(0, 9) select sheet[index];
            player = new Player(playerFrames, this);

            //bricks.Add(new Brick(new BoundingRectangle(0, 12, 84, 32), sheet[9], RandomColor()));
            //bricks.Add(new Brick(new BoundingRectangle(84, 12, 84, 32), sheet[9], RandomColor()));
            //bricks.Add(new Brick(new BoundingRectangle(168, 12, 84, 32), sheet[9], RandomColor()));
            //bricks.Add(new Brick(new BoundingRectangle(252, 12, 84, 32), sheet[9], RandomColor()));
            PopulateBricks();

            //HAD to do this because for some reason my spriteSheet wont update with new changes when rebuilt
            ball = new Ball(new Sprite(new Rectangle(0,0,32,32), balltexture), this);

            world = new AxisList();
            foreach (Brick brick in bricks)
            {
                world.AddGameObject(brick);
            }
            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.Update(gameTime);
            // TODO: Add your update logic here
            var platformQuery = world.QueryRange(ball.Bounds.Y, ball.Bounds.Y + ball.Bounds.Height);
            ball.CheckForBrickCollision(platformQuery);
            ball.CheckForPlayerCollision(player);
            ball.Update(gameTime);

            for(int i = 0; i<bricks.Count; i++)
            {
                if (bricks[i].broken){
                    bricks.RemoveAt(i);
                    i--;
                }
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            bricks.ForEach(brick =>
            {
                brick.Draw(spriteBatch);
            });
            ball.Draw(spriteBatch);
            player.Draw(spriteBatch);
            //spriteBatch.Draw(sheet[11].texture, sheet[11].source, new Rectangle(200, 200, 200, 200), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void PopulateBricks()
        {
            for (int j = 10; j < 36 * 4; j += 36)
            {
                for (int i = 10; i < GraphicsDevice.Viewport.Width - 84; i += 84)
                {
                    bricks.Add(new Brick(new BoundingRectangle(i, j, 84, 32), sheet[9], RandomColor()));
                }
            }
        }
        public Color RandomColor()
        {
            int r = random.Next(6);
            if (r == 0)
            {
                return Color.Red;
            }
            else if (r == 1)
            {
                return Color.Orange;
            }
            else if (r == 2)
            {
                return Color.Yellow;
            }
            else if (r == 3)
            {
                return Color.Green;
            }
            else if (r == 4)
            {
                return Color.Blue;
            }
            else if (r == 5)
            {
                return Color.Indigo;
            }
            else if (r == 6)
            {
                return Color.Purple;
            }
            else { return Color.Magenta; }
        }


        }
}
