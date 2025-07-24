using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace GoUp.Graphics
{
    class Animation
    {
        public Animation()
        {
            _frames = new List<AnimationFrame>();
            _timePassed = 0;
        }
        public bool IsPLaying { get; private set; } = false;
        public bool IsLooping { get; set; } = true;


        public void Start()
        {
            if (!IsPLaying)
            {
                this.IsPLaying = true;
                _timePassed = 0;
            }
        }
        public void Stop()
        {
            this.IsPLaying = false;
        }
        public void Draw(SpriteBatch spriteBatch , Vector2 position)
        {
            if (IsPLaying)
            {
                if(currentFrame != null)
                {
                    currentFrame.Sprite.Draw(spriteBatch, position);
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            if (IsPLaying)
            {
                _timePassed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                bool animationEnd = duration <= _timePassed;

                if (animationEnd && IsLooping)
                {
                    _timePassed -= duration;
                }
                if(animationEnd && !IsLooping)
                {
                    Stop();
                }

            }
        }
        public void AddFrame(Sprite sprite , float timeSpan)
        {
            _frames.Add(new AnimationFrame(sprite, timeSpan));
        }
        private AnimationFrame currentFrame
        {
            get
            {
              if( _frames.Count == 0 ) return null;
              return _frames.
                        Where(f => f.TimeSpan <= _timePassed).
                        OrderBy(f => f.TimeSpan).
                        LastOrDefault();
            }
        }

        private float duration
        {
            get
            {
                if (_frames.Count == 0) return 0;
                return _frames.
                    OrderBy(f => f.TimeSpan).
                    LastOrDefault().
                    TimeSpan;
            }

        }

        private List<AnimationFrame> _frames;
        private float _timePassed;

    }
}
