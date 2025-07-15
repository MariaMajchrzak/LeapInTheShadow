using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoUp.Graphics
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

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Texture2D Texture { get; set; }
        public Color TintColor { get; set; } = Color.White;


        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(this.Texture, position, new Rectangle(this.X, this.Y, this.Width, this.Height), this.TintColor);
        }


    }
}
