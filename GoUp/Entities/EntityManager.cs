using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoUp.Entities
{
    static class EntityManager
    {
        public static void RemoveEntity(IEntity entity)
        {
            _toRemoveEntities.Add(entity);
        }
        public static void AddEntity(IEntity entity)
        {
            _toAddEntities.Add(entity);
        }
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(IEntity entity in _entities)
            {
                entity.Draw(gameTime, spriteBatch);
            }
        }
        public static void Update(GameTime gameTime)
        {
            foreach(IEntity entity in _entities)
            {
                entity.Update(gameTime);
            }
            foreach(IEntity removeEntity in _toRemoveEntities)
            {
                _entities.Remove(removeEntity);
            }
            foreach (IEntity addEntity in _toAddEntities)
            {
                _entities.Add(addEntity);
            }

            _toRemoveEntities.Clear();
            _toAddEntities.Clear();
        }

        private static List<IEntity> _entities = new List<IEntity>();
        private static List<IEntity> _toAddEntities = new List<IEntity>();
        private static List<IEntity> _toRemoveEntities = new List<IEntity>();



    }
}
