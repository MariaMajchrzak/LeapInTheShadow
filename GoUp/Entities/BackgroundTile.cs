using GoUp.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GoUp.Entities
{
    class BackgroundTile
    {
        public BackgroundTile(Sprite sprite , Vector2 position)
        {
            this.Sprite = sprite;
            this.Position = position;   
        }

        public Vector2 Position { get; set; }
        public Sprite Sprite { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position);
        }

    }
}
