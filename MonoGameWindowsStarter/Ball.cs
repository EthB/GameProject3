using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// A class representing a platform
    /// </summary>
    public class Ball : IBoundable
    {
        Random random;
        /// <summary>
        /// The platform's bounds
        /// </summary>
        BoundingRectangle bounds;
        Game game;

        bool started;



        /// <summary>
        /// The platform's sprite
        /// </summary>
        Sprite sprite;

        Vector2 origin = new Vector2(10, 10);
        SpriteEffects spriteEffects;
        float speed = 5;

        public Vector2 Position = new Vector2(236, 360);
        public Vector2 direction = new Vector2(0, 1);
        /// <summary>
        /// The bounding rectangle of the 
        /// </summary>
        public BoundingRectangle Bounds => new BoundingRectangle(Position, 8, 21);

        /// <summary>
        /// Constructs a new platform
        /// </summary>
        /// <param name="bounds">The platform's bounds</param>
        /// <param name="sprite">The platform's sprite</param>
        public Ball(Sprite sprite, Game game)
        {
            this.bounds.X = Position.X;
            this.bounds.Y = Position.Y;
            this.bounds.Width = sprite.Width;
            this.bounds.Height = sprite.Height;
            this.sprite = sprite;
            random = new Random();
            this.game = game;
            started = false;
        }

        public void Update(GameTime gameTime)
        {
            if (started)
            {
                Position.Y += direction.Y * 0.1f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;
                Position.X += direction.X * 0.1f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;
                bounds.X += direction.X * 0.1f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;
                bounds.Y += direction.Y * 0.1f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;
                if (bounds.X < 0)
                {
                    direction.X *= -1;
                    bounds.X = 1;
                }
                if (bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width)
                {
                    direction.X *= -1;
                    bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width - 1;
                }
                if (bounds.Y < 0)
                {
                    direction.Y *= -1;
                    bounds.Y = 1;
                }
                if (bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height)
                {
                    direction.Y *= -1;
                    bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height - 1;
                    game.Exit();
                }
            }
            else {
                var keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    started = true;
                    direction = new Vector2(0, -1) * speed;
                    direction.Normalize();
                }
                else if (keyboardState.IsKeyDown(Keys.Left))
                {
                    Position.X -= 10;
                    bounds.X -= 10;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    Position.X += 10;
                    bounds.X += 10;
                }
            }
        }

        public void CheckForBrickCollision(IEnumerable<IBoundable> bricks)
        {
            //Debug.WriteLine($"Checking collisions against {platforms.Count()} platforms");

            foreach (Brick brick in bricks)
            {
                if (Bounds.CollidesWith(brick.Bounds))
                {
                    if (!brick.broken)
                    {
                        brick.broken = true;
                        direction = (new Vector2((brick.Bounds.X + brick.Bounds.Width / 2) - (bounds.X + bounds.Width / 2), brick.Bounds.Y - bounds.Y) * .06f * -1);
                        direction.Normalize();
                    }
                    
                    
                }
            }

        }

        public void CheckForPlayerCollision(Player player)
        {
            if (Bounds.CollidesWith(player.Bounds))
            {
                direction = new Vector2((player.Bounds.X + player.Bounds.Width/2) - (bounds.X + bounds.Width/2), player.Bounds.Y - bounds.Y) * .06f * -1;
                direction.Normalize();
            }
        }

        /// <summary>
        /// Draws the platform
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch to render to</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //VisualDebugging.DrawRectangle(spriteBatch, bounds, Color.White);
            //spriteBatch.Draw(sprite.texture, bounds, Color.Green);
            sprite.Draw(spriteBatch, Position, Color.White, 0, origin, 2, spriteEffects, 0);
            //sprite.Draw(spriteBatch, Position, Color.White);
            //spriteBatch.Draw(sprite.texture, bounds, Color.White);
        }
    }
}
