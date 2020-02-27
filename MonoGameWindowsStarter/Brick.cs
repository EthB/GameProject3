using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter
{

    public enum BrickState
    {
        broken,
        damaged,
        cool
    }
    /// <summary>
    /// A class representing a brick
    /// </summary>
    public class Brick : IBoundable
    {
        /// <summary>
        /// The brick's bounds
        /// </summary>
        BoundingRectangle bounds;

        /// <summary>
        /// The brick's sprite
        /// </summary>
        Sprite sprite;

        //public bool broken;
        Vector2 origin = new Vector2(21/4, 12);
        SpriteEffects spriteEffects;
        public BrickState brickState;

        /// <summary>
        /// The bounding rectangle of the 
        /// </summary>
        public BoundingRectangle Bounds => bounds;
        Color color;
        /// <summary>
        /// Constructs a new Brick
        /// </summary>
        /// <param name="bounds">The platform's bounds</param>
        /// <param name="sprite">The platform's sprite</param>
        public Brick(BoundingRectangle bounds, Sprite sprite, Color color)
        {
            this.bounds = bounds;
            this.sprite = sprite;
            brickState = BrickState.cool;
            this.color = color;
            
        }


        /// <summary>
        /// Draws the brick
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch to render to</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(sprite.texture, new Vector2(200,200), Color.White);
            if (brickState == BrickState.cool)
            {
                sprite.Draw(spriteBatch, new Vector2(bounds.X, bounds.Y), color, 0, origin, 4, spriteEffects, 1);
                //sprite.Draw(spriteBatch, bounds, Color.White);
            }
            

        }
    }
}
