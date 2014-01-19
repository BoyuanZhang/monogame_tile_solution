using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using TileEngine.Layer.Tiles;

namespace TileEngine.Layer
{
    public class EditorTileLayer : TileLayer
    {
        private EditorTile[,] m_layerLayout;

        //rectangle every tile uses in drawing to reduce rectangle data on the heap... G/C would make this unnoticeable, but feels better to manipulate only position
        //instead of an entire new rectangle every draw
        protected Rectangle m_tileRectangle;
        
        public EditorTileLayer(int width, int height, LayerType.LayerTypesEnum layerType)
            : base(width, height, layerType)
        {
            m_layerLayout = new EditorTile[width, height];

            //Set tile layer layout to be empty
            for (int x = 0; x < m_layoutWidth; x++)
                for (int y = 0; y < m_layoutHeight; y++)
                    m_layerLayout[x, y] = new EditorTile(EMPTYTILEINDEX, new Point(x * TileWidth, y * TileHeight));

            //initialize tile rectangle, only it's position will be changing in draws, not it's width or height unless tilewidth, or tileheight we changed
            m_tileRectangle = new Rectangle(0, 0, TileLayer.TileWidth, TileLayer.TileHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //No need for the tile editor yet
        }

        //For the editor we draw everything (might be good for mini-map of tile map so we can have an overhead view)
        //The OffsetPositionVector could be the position vector of a editor camera vector, or just the scroll bar offsets
        public void Draw(SpriteBatch spriteBatch, Vector2 screenPosition)
        {
            //sprite batch begin should be called before entering the tile layer draw

            //Iterate through each tile and draw it accordingly
            //Since the editor tile layers are not partitioned for optimization like in game mode we just draw every tile. This can be useful for when we 
            //draw mini-maps dynamically of the changing edited tile layer
            for (int x = 0; x < m_layoutWidth; x++)
            {
                for (int y = 0; y < m_layoutHeight; y++)
                {
                    int textureIndex = GetCellIndex(x, y);
                    if (textureIndex != EMPTYTILEINDEX)
                    {
                        Texture2D texture = m_tileTextureList[textureIndex];
                        //change the position of the rectangle where tile will be drawn
                        m_tileRectangle.X = GetCellPosition(x, y).X - (int)screenPosition.X;
                        m_tileRectangle.Y = GetCellPosition(x,y).Y - (int)screenPosition.Y;
                        
                        spriteBatch.Draw(texture, m_tileRectangle, Color.White * m_alphaChannel);
                    }
                }
            }
        }

        public Point GetCellPosition(int row, int column)
        {
            return m_layerLayout[row, column].TilePosition;
        }

        public override void SetCellIndex(int row, int column, int newTextureIndex)
        {
            m_layerLayout[row, column].TextureIndex = newTextureIndex;
        }

        public override int GetCellIndex(int row, int column)
        {
            return m_layerLayout[row, column].TextureIndex;
        }
    }
}
