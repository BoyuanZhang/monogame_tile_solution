using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Must implement the partition tree space interface so tiles may be used inside of our partition tree
using TileEngine.Partitioning;

namespace TileEngine.Layer.Tiles
{
    //For future use, currently
    //Hope is, in the future when we have a proper file system for layers (maybe XML, currently we are using plain old text), tile properties can be easily imported
    //so we can use more advanced tiles.
    public class Tile
    {
        //the Layer's hold the list of all textures used within the layout, a tile object holds the index to that texture
        protected int m_tileTextureIndex;

        //Initialization a tile requires specification of its type and a texture for the tile
        public Tile( int textureIndex )
        {
            m_tileTextureIndex = textureIndex;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture, float alphaChannel) { }

        //You can get / set the index, other properties are read-only
        public int TextureIndex { get { return m_tileTextureIndex; } set { m_tileTextureIndex = value; } }
    }
}
