using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TileEngine.Layer.Tiles;

namespace TileEngine.Layer
{
    public class GameTileLayer : TileLayer
    {
        private GameTile[,] m_layerLayout;

        public GameTileLayer(int width, int height, LayerType.LayerTypesEnum layerType)
            : base(width, height, layerType)
        {
            m_layerLayout = new GameTile[width, height];

            //Set tile layer layout to be empty
            for (int x = 0; x < m_layoutWidth; x++)
                for (int y = 0; y < m_layoutHeight; y++)
                    m_layerLayout[x, y] = new GameTile(EMPTYTILEINDEX, new Point(x * TileWidth, y * TileHeight));
        }

        //TODO: *** In draw, perform optimizations to only draw tiles that are on the current screen
        //*** Good optimization would be to put tile maps into a partition tree, get all tiles in the partitions visible to the user, and only draw those
        public override void Draw(SpriteBatch spriteBatch)
        {
            //When we modify this for optimization, here's where we should get the relevent cells to be drawn from the partition object (partition tee).
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
                        //Later on we will most likely have to factor in the camera vector for this draw.. but until we hit that bridge leave this here
                        m_layerLayout[x, y].Draw(spriteBatch, texture, m_alphaChannel);
                    }
                }
            }
            //**** PLACEHOLDER CODE ****** (to be honest this might be good for the editor tile layer draw, to draw everything, and 
            //we only optimize the GameTileLayer draw method
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
