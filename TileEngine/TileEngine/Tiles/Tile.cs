using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileEngine.Tiles
{
    //For future use, currently, we just use a texture to represent the tile.
    //Hope is, in the future when we have a proper file system for layers (maybe XML, currently we are using plain old text), tile properties can be easily imported
    //so we can use more advanced tiles.
    public class Tile
    {
        //Specifies the type of the tile so different effects can be applied on these tiles
        public enum TileType { NORMAL, COLLIDABLE, SLOW };

        protected static int m_tileWidth = 64;
        protected static int m_tileHeight = 64;

        protected TileType m_tileType;
        protected Texture2D m_tileTexture;

        //Initialization a tile requires specification of its type and a texture for the tile
        public Tile( TileType type, Texture2D texture)
        {
            m_tileType = type;
            m_tileTexture = texture;
        }

        public static int TileWidth
        {
            get { return m_tileWidth; }
            set
            {
                m_tileWidth = (int)MathHelper.Clamp(value, 20f, 100f);
            }
        }

        public static int TileHeight
        {
            get { return m_tileHeight; }
            set
            {
                m_tileHeight = (int)MathHelper.Clamp(value, 20f, 100f);
            }
        }
    }
}
