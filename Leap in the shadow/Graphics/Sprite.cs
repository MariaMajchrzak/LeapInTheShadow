using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeapInTheSadow.Graphics
{
    class Sprite
    {
        public Sprite(int x, int y, int width, int height, Texture2D spritesheet) 
        { 
            this.X = x;
            this.Y = y; 
            this.Width = width;
            this.Height = height;
            this.Texture = spritesheet;
        }
        public Sprite(int x, int y, int width, int height, Texture2D spritesheet,float scale)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Texture = spritesheet;
            this.Scale = scale;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Scale { get; set; } = 1.0f;
        public float Rotation { get; set; } = 0f;
        public Texture2D Texture { get; set; }
        public Color TintColor { get; set; } = Color.White;


        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(
                this.Texture, 
                position,
                new Rectangle(this.X, this.Y, this.Width, this.Height),
                this.TintColor,
                this.Rotation,
                Vector2.Zero,
                this.Scale,
                SpriteEffects.None,
                0f
                );
        }


    }
}
