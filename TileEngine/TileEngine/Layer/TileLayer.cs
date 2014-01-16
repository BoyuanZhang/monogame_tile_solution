using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TileEngine.Tiles;
using TileEngine.Map;

namespace TileEngine.Layer
{
    public class TileLayer
    {
        public const int EMPTYTILEINDEX = -1;

        //Number of tiles in the width and height of layout
        protected int m_layoutWidth;
        protected int m_layoutHeight;

        //Tile width, and height parameters... however in the future these will probably be moved to the Tile class when tile's become more dynamic
        public static int TileWidth = 64;
        public static int TileHeight = 64;

        protected int[,] m_tileLayerLayout;
        //List of textures used in tile layer layout
        protected List<Texture2D> m_tileTextureList;
    
        protected float m_alphaChannel;

        //rectangle every tile uses in drawing to reduce rectangle data on the heap... G/C would make this unnoticeable, but feels better to manipulate only position
        //instead of an entire new rectangle every draw
        protected Rectangle m_tileRectangle;

        public TileLayer(int width, int height)
        {
            m_tileLayerLayout = new int[width, height];
            m_tileTextureList = new List<Texture2D>();
            
            m_layoutWidth = width;
            m_layoutHeight = height;

            //Set tile layer layout to be empty
            for (int x = 0; x < m_layoutWidth; x++)
                for (int y = 0; y < m_layoutHeight; y++)
                    SetCellIndex(x, y, EMPTYTILEINDEX);

            m_alphaChannel = 1.0f;

            //initialize tile rectangle, only it's position will be changing in draws, not it's width or height unless tilewidth, or tileheight we changed
            m_tileRectangle = new Rectangle(0, 0, Tile.TileWidth, Tile.TileHeight);
        }

        public virtual void Draw( SpriteBatch spriteBatch )
        {
        }

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

        //Getters of setters of cells within the tile layout. Validation of row, column values should occur before calling this function
        public void SetCellIndex(int row, int column, int newTextureIndex)
        {
            m_tileLayerLayout[row, column] = newTextureIndex;
        }

        public int GetCellIndex(int row, int column)
        {
            return m_tileLayerLayout[row, column];
        }

        public float Alpha { get { return m_alphaChannel; } set { m_alphaChannel = value; } }
        public int LayoutWidth { get { return m_layoutWidth; } }
        public int LayoutHeight { get { return m_layoutHeight; } }
    }
}
