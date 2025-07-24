using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoUp.Graphics
{
    class AnimationFrame
    {
        public AnimationFrame(Sprite sprite, float timeSpan)
        {
            Sprite = sprite;
            TimeSpan = timeSpan;
        }
        public Sprite Sprite { get; set; }
        public float TimeSpan { get; set; }
    }
}
