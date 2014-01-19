using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TileEngine.Layer.Tiles;
using TileEngine.Map;

namespace TileEngine.Layer
{
    public class TileLayer
    {
        public const int EMPTYTILEINDEX = -1;

        //Number of tiles in the width and height of layout
        protected int m_layoutWidth;
        protected int m_layoutHeight;

        //public properties to modify / get the dimensions used for the tiles in all Tile layers
        public static int TileWidth = 64;
        public static int TileHeight = 64;

        //List of textures used in tile layer layout
        protected List<Texture2D> m_tileTextureList;

        protected LayerType.LayerTypesEnum m_layerType;

        protected float m_alphaChannel;

        public TileLayer(int width, int height, LayerType.LayerTypesEnum layerType)
        {
            m_tileTextureList = new List<Texture2D>();
            
            m_layoutWidth = width;
            m_layoutHeight = height;

            m_layerType = layerType;

            m_alphaChannel = 1.0f;
        }

        public virtual void Draw(SpriteBatch spriteBatch) { } 

        //Add new texture if it is not inside the texture list
        public void AddNewTexture(Texture2D texture)
        {
            if (!m_tileTextureList.Contains(texture))
                m_tileTextureList.Add(texture);
        }

        //Helper function that returns index of texture if it is used, or -1 if it is not
        public int UsedTextureIndex(Texture2D texture)
        {
            if (m_tileTextureList.Contains(texture))
                return m_tileTextureList.IndexOf(texture);

            return EMPTYTILEINDEX;
        }

        //Remove all uses of a texture from this layer
        public void RemoveTexture(Texture2D texture)
        {
            int textureIndex = UsedTextureIndex(texture);

            if (textureIndex != EMPTYTILEINDEX)
            {
                for (int x = 0; x < m_layoutWidth; x++)
                {
                    for (int y = 0; y < m_layoutHeight; y++)
                    {
                        if (GetCellIndex(x, y) == textureIndex)
                        {
                            SetCellIndex(x, y, -1);
                        }
                        else
                        {
                            //every index greater than the texture index to be removed must be decremented by 1
                            //since after the texture is removed from the tile texture list the indexes greater than
                            //this texture index will also be shuffled down.
                            int cellIndex = GetCellIndex(x, y);
                            if (cellIndex > textureIndex)
                            {
                                SetCellIndex(x, y, cellIndex - 1);
                            }
                        }
                    }
                }

                m_tileTextureList.Remove(texture);
            }
        }

        //Child layers should implement the way their cells are retrieved and set
        public virtual void SetCellIndex(int row, int column, int newTextureIndex) { }
        //if the parent get cell is called for whatever reason.. just return an empty index
        public virtual int GetCellIndex(int row, int column) { return EMPTYTILEINDEX; }

        public float Alpha { get { return m_alphaChannel; } set { m_alphaChannel = value; } }

        //Read only properties

        public LayerType.LayerTypesEnum LayerLayoutType { get { return m_layerType; } }
        public int LayoutWidth { get { return m_layoutWidth; } }
        public int LayoutHeight { get { return m_layoutHeight; } }
    }
}
