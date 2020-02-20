using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// An enumeration of possible player animation states
    /// </summary>
    enum PlayerAnimState
    {
        Idle,
        Right,
        Left
    }
    

    /// <summary>
    /// A class representing the player
    /// </summary>
    public class Player
    {
        const int FRAME_RATE = 250;

        Sprite[] frames;

        int currentFrame = 0;

        PlayerAnimState animationState = PlayerAnimState.Idle;

        int speed = 10;

        public Vector2 origin = new Vector2(3, 14);

        TimeSpan animationTimer;

        SpriteEffects spriteEffects = SpriteEffects.None;

        Color color = Color.White;

        bool idleforward;

        Game game;



        /// <summary>
        /// Gets and sets the position of the player on-screen
        /// </summary>
        public Vector2 Position = new Vector2(200, 400);

        public BoundingRectangle Bounds => new BoundingRectangle(Position, 104, 16);

        /// <summary>
        /// Constructs a new player
        /// </summary>
        /// <param name="frames">The sprite frames associated with the player</param>
        public Player(IEnumerable<Sprite> frames, Game game)
        {
            this.frames = frames.ToArray();
            animationState = PlayerAnimState.Idle;
            idleforward = true;
            this.game = game;
        }

        /// <summary>
        /// Updates the player, applying movement
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
           
            


            // Horizontal movement
            if (keyboard.IsKeyDown(Keys.Left))
            {
                
                if(Position.X - Bounds.Width/4 > 0)
                {
                    animationState = PlayerAnimState.Left;
                    Position.X -= speed;
                }
                else { animationState = PlayerAnimState.Idle; }
                
            }
            else if(keyboard.IsKeyDown(Keys.Right))
            {
                
                if(Position.X + Bounds.Width/4 < game.GraphicsDevice.Viewport.Width)
                {
                    animationState = PlayerAnimState.Right;
                    Position.X += speed;
                }
                else { animationState = PlayerAnimState.Idle; }
                
            }
            else
            {
                animationState = PlayerAnimState.Idle;
            }
            

            // Apply animations
            switch (animationState)
            {
                case PlayerAnimState.Idle:
                    animationTimer += gameTime.ElapsedGameTime;
                    if (currentFrame == 7 || currentFrame == 8 || currentFrame <0) {
                        currentFrame = 0;
                    }
                    
                    if (animationTimer.TotalMilliseconds > FRAME_RATE)
                    {
                        
                        if (currentFrame < 6 && idleforward)
                        {
                            currentFrame++;
                        }
                        else if(currentFrame >=0 && !idleforward)
                        {
                            currentFrame--;
                        }
                        if (currentFrame >= 6)
                        {
                            idleforward = false;
                        }
                        else if(currentFrame <= 0)
                        {
                            idleforward = true;
                        }
                        animationTimer = new TimeSpan(0);
                    }
                    
                    
                    break;

                

                case PlayerAnimState.Left:
                    currentFrame = 7;
                    break;

                case PlayerAnimState.Right:
                    currentFrame = 8;
                    break;

            }
            if( currentFrame < 0)
            {
                currentFrame = 0;
            }
        }

        

        /// <summary>
        /// Render the player sprite.  Should be invoked between 
        /// SpriteBatch.Begin() and SpriteBatch.End()
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            frames[currentFrame].Draw(spriteBatch, Position, color, 0, origin, 4, spriteEffects, 0);
            //spriteBatch.Draw(frames[1].texture, Bounds, Color.White);

        }

    }
}
