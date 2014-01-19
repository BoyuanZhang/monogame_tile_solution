using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TileEditor.Handlers;
using TileEditor.Controls;

using TileEngine.Layer;

namespace TileEditor.Manager
{
    public class GraphicsManager
    {
        //handle to main graphics device
        private GraphicsDevice m_graphicsDevice;
        //Graphics manager needs various data for displaying different objects... currently just needs the tile layer handler
        private DataManager m_dataManager;
        //Handle to the actual graphics control this manager is managing
        //in the future, this probably should not be a specific graphics control
        private GraphicsEditor m_graphicsControl;

        private int m_displayWidth;
        private int m_displayHeight;

        //The position of the screen inside of the tile map
        private Vector2 m_screenPosition;

        public GraphicsManager(GraphicsEditor graphicsEditor, DataManager dataManager)
        {
            m_graphicsControl = graphicsEditor;
            m_graphicsDevice = graphicsEditor.GraphicsDevice;
            m_dataManager = dataManager;

            m_displayWidth = m_graphicsDevice.PresentationParameters.BackBufferWidth;
            m_displayHeight = m_graphicsDevice.PresentationParameters.BackBufferHeight;

            m_screenPosition = Vector2.Zero;
        }

        public void Draw( SpriteBatch spriteBatch, List<string> selectedKeys, bool drawTileOutlines )
        {
            m_graphicsDevice.Clear(Color.DarkCyan);
            //We will need handle to the tile layer handler to draw the layer layouts
            TileLayerHandler tileLayerHandler = m_dataManager.TileLayerDataHandle;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            //Only draw selected tile layer layouts
            foreach (string key in selectedKeys)
            {
                if (tileLayerHandler.ContainsLayer(key))
                {
                    EditorTileLayer tileLayer = tileLayerHandler.GetLayer(key);
                    //Tell the tile engine to draw the layer layout, but also let the engine know the position the screen currently
                    tileLayer.Draw(spriteBatch, m_screenPosition);

                    if (m_dataManager.EditorOutlineTexture != null)
                    {
                        //Now if tile outlining is checked we draw the tile outline of all empty tile cells 
                        if (drawTileOutlines)
                        {
                            for (int x = 0; x < tileLayer.LayoutWidth; x++)
                            {
                                for (int y = 0; y < tileLayer.LayoutHeight; y++)
                                {
                                    //Is this cell empty?
                                    if (tileLayer.GetCellIndex(x, y) == TileLayer.EMPTYTILEINDEX)
                                    {
                                        spriteBatch.Draw(m_dataManager.EditorOutlineTexture,
                                                            new Rectangle(x * TileLayer.TileWidth - (int)m_screenPosition.X, y * TileLayer.TileHeight - (int)m_screenPosition.Y,
                                                                            TileLayer.TileWidth, TileLayer.TileHeight),
                                                                            Color.White);
                                    }
                                }
                            }
                        }

                        //Draw the outline of the cell the user is currently hovering over on the current layer, if there is no current layer
                        //or the mouse is outside the graphics control nothing will be drawn. The tile layer handler handles whether there is a current layer or not
                        if (tileLayerHandler.CellX != -1 || tileLayerHandler.CellY != -1)
                        {
                            spriteBatch.Draw(m_dataManager.EditorOutlineTexture,
                                                new Rectangle(tileLayerHandler.CellX * TileLayer.TileWidth - (int)m_screenPosition.X,
                                                                tileLayerHandler.CellY * TileLayer.TileHeight - (int)m_screenPosition.Y,
                                                                TileLayer.TileWidth, TileLayer.TileHeight),
                                                Color.Red);
                        }
                    }
                }
            }

            spriteBatch.End();
        }

        //Update the position of the screen within the layer map, and update the cell the user is currently hovering over

        //ya.. so there's an MonoGame Point, and a System.Drawing Point... we'll just use the System.Drawing Point class
        public void Update(int hScrollBarValue, int vScrollBarValue, System.Drawing.Point mousePosition )
        {
            TileLayerHandler tileLayerHandler = m_dataManager.TileLayerDataHandle;

            m_screenPosition.X = hScrollBarValue * EditorTileLayer.TileWidth;
            m_screenPosition.Y = vScrollBarValue * EditorTileLayer.TileHeight;

            int mousePosX = m_graphicsControl.PointToClient(mousePosition).X;
            int mousePosY = m_graphicsControl.PointToClient(mousePosition).Y;

            if (mousePosX >= 0 && mousePosX < m_displayWidth && mousePosY >= 0 && mousePosY < m_displayHeight)
            {
                tileLayerHandler.SetCurrentCellX(mousePosX, hScrollBarValue);
                tileLayerHandler.SetCurrentCellY(mousePosY, vScrollBarValue);
            }
            else
            {
                tileLayerHandler.SetCellsOutsideBound();
            }
        }
   
        public void ClearControlScreen()
        {
            m_graphicsDevice.Clear(Color.DarkCyan);
        }
    }
}
