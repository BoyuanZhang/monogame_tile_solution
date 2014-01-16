using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TileEngine.Tiles;

namespace TileEngine.Layer
{
    public class GameTileLayer : TileLayer
    {
        //Not sure if this will be used for making a tile layer dynamically in game?
        public GameTileLayer(int width, int height)
            : base(width, height)
        {

        }

        //TODO: *** In draw, perform optimizations to only draw tiles that are on the current screen
        //*** Good optimization would be to put tile maps into a partition tree, get all tiles in the partitions visible to the user, and only draw those
        public override void Draw(SpriteBatch spriteBatch)
        {
            //**** PLACEHOLDER CODE ONLY, engine draw function needs optimization

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
                        m_tileRectangle.X = x * Tile.TileWidth;
                        m_tileRectangle.Y = y * Tile.TileHeight;

                        spriteBatch.Draw(texture, m_tileRectangle, Color.White * m_alphaChannel);
                    }
                }
            }
            //**** PLACEHOLDER CODE ****** (to be honest this might be good for the editor tile layer draw, to draw everything, and 
            //we only optimize the GameTileLayer draw method
        }
    }
}
