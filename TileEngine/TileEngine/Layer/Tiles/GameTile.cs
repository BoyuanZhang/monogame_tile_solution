using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TileEngine.Layer.Tiles
{
    public class GameTile : Tile
    {
        //Rectangle represents the actual tile rectangle that will used for drawing / collision etc...
        private Rectangle m_tileRectangle;

        public GameTile(int textureIndex, Point position) : base( textureIndex )
        {
            m_tileRectangle = new Rectangle(position.X, position.Y, TileLayer.TileWidth, TileLayer.TileHeight);
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture, float alphaChannel)
        {
            spriteBatch.Draw(texture, m_tileRectangle, Color.White * alphaChannel);
        }
    }
}
