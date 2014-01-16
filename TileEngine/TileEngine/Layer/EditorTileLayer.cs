using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using TileEngine.Tiles;

namespace TileEngine.Layer
{
    public class EditorTileLayer : TileLayer
    {
        public EditorTileLayer(int width, int height)
            : base(width, height)
        {

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
            for (int x = 0; x < m_layoutWidth; x++)
            {
                for (int y = 0; y < m_layoutHeight; y++)
                {
                    int textureIndex = GetCellIndex(x, y);
                    if (textureIndex != EMPTYTILEINDEX)
                    {
                        Texture2D texture = m_tileTextureList[textureIndex];
                        //change the position of the rectangle where tile will be drawn
                        m_tileRectangle.X = x * Tile.TileWidth - (int)screenPosition.X;
                        m_tileRectangle.Y = y * Tile.TileHeight - (int)screenPosition.Y;

                        spriteBatch.Draw(texture, m_tileRectangle, Color.White * m_alphaChannel);
                    }
                }
            }
        }
    }
}
