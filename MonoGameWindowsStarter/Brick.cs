using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// A class representing a platform
    /// </summary>
    public class Brick : IBoundable
    {
        /// <summary>
        /// The platform's bounds
        /// </summary>
        BoundingRectangle bounds;

        /// <summary>
        /// The platform's sprite
        /// </summary>
        Sprite sprite;

        public bool broken;
        Vector2 origin = new Vector2(21/4, 12);
        SpriteEffects spriteEffects;

        /// <summary>
        /// The bounding rectangle of the 
        /// </summary>
        public BoundingRectangle Bounds => bounds;
        Color color;
        /// <summary>
        /// Constructs a new platform
        /// </summary>
        /// <param name="bounds">The platform's bounds</param>
        /// <param name="sprite">The platform's sprite</param>
        public Brick(BoundingRectangle bounds, Sprite sprite, Color color)
        {
            this.bounds = bounds;
            this.sprite = sprite;
            broken = false;
            this.color = color;
            
        }

        public void Destroy()
        {
            
        }

        /// <summary>
        /// Draws the platform
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch to render to</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(sprite.texture, bounds, Color.White);
            VisualDebugging.DrawRectangle(spriteBatch, bounds, Color.Green);
            if (!broken)
            {
                sprite.Draw(spriteBatch, new Vector2(bounds.X, bounds.Y), color, 0, origin, 4, spriteEffects, 1);
                //sprite.Draw(spriteBatch, bounds, Color.White);
            }
            

        }
    }
}
