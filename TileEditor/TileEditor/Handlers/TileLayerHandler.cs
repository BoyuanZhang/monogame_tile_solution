using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TileEngine.Layer;
using TileEngine.Layer.DataObjects;

namespace TileEditor.Handlers
{
    public class TileLayerHandler : Handler
    {
        //Current selected layer on the client, changes whenever the user selects a new layer.
        //However if multiple layers are selected the current layer will always be the top most layer selected on the list
        private EditorTileLayer m_currentLayer;

        private Dictionary<string, EditorTileLayer> m_tileLayerDictionary;

        //Data that tells us which Cell inside the current layer the user is hovering over (Always refers to cells of current layer!)
        //Not sure if this logic belongs here, but will put here now as a place holder
        private int m_cellX;
        private int m_cellY;

        public TileLayerHandler() 
        {
            //current layer will start off empty
            m_currentLayer = null;
            //Cells will be -1 to mean no layer cells being hovered over
            m_cellX = -1;
            m_cellY = -1;

            m_tileLayerDictionary = new Dictionary<string, EditorTileLayer>();
        }

        public override bool HandleFile(string fileName)
        {
            //Not yet implemented
            return false;
        }

        public Tuple< string, List<string> > HandleExistingLayer(string fileName, TextureHandler textureHandler, TexturePreviewHandler texturePreviewHandler)
        {
            try
            {
                //Make sure file doesn't already exist
                string trimmedFileName = Utility.FileUtility.GetFileNameWithParentFolder(fileName);
                if (!m_tileLayerDictionary.ContainsKey(trimmedFileName))
                {
                    //Get the tile layer data object

                    EditorTileLayerDO openedTileLayerDO = FileHandling.LayerFileHandler.OpenEditorLayer(fileName, textureHandler, texturePreviewHandler);
                    if (openedTileLayerDO != null)
                    {
                        m_tileLayerDictionary.Add(trimmedFileName, openedTileLayerDO.EditorTileLayer);

                        //we grabbed necessary information from the layer data object, now simply return a layer tuple, that contains the layer name,
                        //and a list of all the layers texture names
                        return new Tuple<string, List<string>>(trimmedFileName, openedTileLayerDO.LayerTextureNameList);
                    }
                }
                //if it does exist, or the layer file could not be handled properly just return an empty tuple

                return new Tuple<string, List<string>>(string.Empty, null);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool HandleNewLayer(string layerName, int width, int height, int layerType)
        {
            if (!m_tileLayerDictionary.ContainsKey(layerName))
            {
                //throw layertype as the enum type, so the layer can store the type it is
                m_tileLayerDictionary.Add(layerName, new EditorTileLayer(width, height, (LayerType.LayerTypesEnum)layerType));
                return true;
            }

            return false;
        }

        public void UpdateCurrentLayer(string layerKey)
        {
            if (m_tileLayerDictionary.ContainsKey(layerKey))
            {
                m_currentLayer = m_tileLayerDictionary[layerKey];
            }
        }

        //If layer exists, remove it. If that layer is the current layer, set current layer to null
        public void RemoveLayer(string layerKey)
        {
            if (m_tileLayerDictionary.ContainsKey(layerKey))
            {
                if (m_currentLayer != null)
                {
                    if (m_currentLayer.Equals(m_tileLayerDictionary[layerKey]))
                        m_currentLayer = null;
                }

                m_tileLayerDictionary.Remove( layerKey );
            }
        }

        //Reset any cells using instances of this texture across all layer layouts
        public void RemoveTextureFromLayers(Texture2D texture)
        {
            foreach (string key in m_tileLayerDictionary.Keys )
            {
                m_tileLayerDictionary[key].RemoveTexture(texture);
            }
        }

        public void SaveLayer(string contentPath, string saveLayerName, string layerKey, List<string> textureFileNames)
        {
            try
            {
                if (m_tileLayerDictionary.ContainsKey(layerKey))
                {
                    FileHandling.LayerFileHandler.SaveLayer( contentPath, saveLayerName, m_tileLayerDictionary[layerKey], textureFileNames);
                }
                else
                {
                    //Could not find key.. this should never happen but in case we'll leave this else statement here to handle these scenarios
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Draw to layer if there is a current layer, and there is a selected cell ( hovered over cell )
        public void DrawToLayer(bool fill, Texture2D texture)
        {
            if (m_currentLayer != null)
            {
                if (m_cellX != -1 || m_cellY != -1)
                {
                    //See if this texture is being used by tile layer currently
                    int usedIndex = m_currentLayer.UsedTextureIndex(texture);
                    int currentIndex = m_currentLayer.GetCellIndex(m_cellX, m_cellY);

                    if (usedIndex != TileLayer.EMPTYTILEINDEX)
                    {
                        //if tile layer is using this texture, and the current cell value is not the same as 
                        //the texture value being used for drawing
                        if (currentIndex != usedIndex)
                        {
                            //Should we fill? or just draw
                            if (fill)
                                FillCells(m_cellX, m_cellY, usedIndex, currentIndex);
                            else
                                m_currentLayer.SetCellIndex(m_cellX, m_cellY, usedIndex);
                        }
                    }
                    else
                    {
                        //tile layer is not using this texture, then add the texture first then find it's index and draw it
                        m_currentLayer.AddNewTexture(texture);
                        usedIndex = m_currentLayer.UsedTextureIndex(texture);

                        //Should never really be true... but nice to have a safety net
                        if (currentIndex != usedIndex)
                        {
                            if (fill)
                                FillCells(m_cellX, m_cellY, usedIndex, currentIndex);
                            else
                                m_currentLayer.SetCellIndex(m_cellX, m_cellY, usedIndex);
                        }
                    }
                }
            }
        }

        public void EraseFromLayer(bool fill)
        {
            if (m_currentLayer != null)
            {
                if (m_cellX != -1 || m_cellY != -1)
                {
                    int currentIndex = m_currentLayer.GetCellIndex(m_cellX, m_cellY);

                    if (currentIndex != -1)
                    {
                        if (fill)
                            FillCells(m_cellX, m_cellY, -1, currentIndex);
                        else
                            m_currentLayer.SetCellIndex(m_cellX, m_cellY, -1);
                    }
                }
            }
        }

        public void SetAlpha(float alpha)
        {
            if (m_currentLayer != null)
            {
                m_currentLayer.Alpha = alpha;
            }
        }

        //Does layer exist in the dictionary?
        public bool ContainsLayer(string layerKey)
        {
            if (m_tileLayerDictionary.ContainsKey(layerKey))
                return true;

            return false;
        }

        //Get layer if it exists, otherwise return null
        public EditorTileLayer GetLayer(string layerKey)
        {
            if (m_tileLayerDictionary.ContainsKey(layerKey))
                return m_tileLayerDictionary[layerKey];

            return null;
        }

        //Gets dimensions of the largest layer of selected layers, if there is no current layer return empty dimensions (0,0)
        public Tuple<int, int> GetLayerDimensions( List<string> layerKeys )
        {
            if (layerKeys.Count > 0)
            {
                //there should always be a current layer, if there are selected layers from the client.
                //The following if statement should never be null.. however it is here as a precaution anyways
                if (m_currentLayer != null)
                {
                    int maxWidth = 0;
                    int maxHeight = 0;

                    foreach (string key in layerKeys)
                    {
                        if (m_tileLayerDictionary.ContainsKey(key))
                        {
                            maxWidth = Math.Max(maxWidth, m_tileLayerDictionary[key].LayoutWidth);
                            maxHeight = Math.Max(maxHeight, m_tileLayerDictionary[key].LayoutHeight);
                        }
                    }

                    return new Tuple<int, int>(maxWidth, maxHeight);
                }
            }

            return new Tuple<int,int>( 0, 0 );
        }

        public int GetLayerAlpha(string layerKey)
        {
            if( m_tileLayerDictionary.ContainsKey( layerKey ) )
            {
                float alpha = m_tileLayerDictionary[layerKey].Alpha;

                return (int)(alpha * 100.0f);
            }

            return 100;
        }

        public string GetLayerTypeAsString(string layerKey)
        {
            if (m_tileLayerDictionary.ContainsKey(layerKey))
            {
                LayerType.LayerTypesEnum layerType = m_tileLayerDictionary[layerKey].LayerLayoutType;

                //convert it to a string according to the actual layer type to pass back
                if (layerType == LayerType.LayerTypesEnum.Normal)
                    return "Normal";
                else if (layerType == LayerType.LayerTypesEnum.Collision)
                    return "Collision";
            }
            return string.Empty;
        }

        //Not sure if this logic belongs in the tile layer handler yet
        //Handles the data of the cell being hovered over on the current selected layer
        #region Current layer cell handlers 

        public int CellX { get { return m_cellX; } }
        public int CellY { get { return m_cellY; } }

        //Update the cells the user is hovering over if there is a layer selected
        public void SetCurrentCellX(int mousePositionX, int horizontalScreenOffset)
        {
            if (m_currentLayer != null)
                m_cellX = (int)MathHelper.Clamp(mousePositionX / TileLayer.TileWidth + horizontalScreenOffset, 0, m_currentLayer.LayoutWidth - 1);
            else
                m_cellX = -1;
        }

        public void SetCurrentCellY(int mousePositionY, int verticalScreenOffset)
        {
            if (m_currentLayer != null)
                m_cellY = (int)MathHelper.Clamp(mousePositionY / TileLayer.TileHeight + verticalScreenOffset, 0, m_currentLayer.LayoutHeight - 1);
            else
                m_cellY = -1;
        }

        public void SetCellsOutsideBound()
        {
            m_cellX = -1;
            m_cellY = -1;
        }

        #endregion

        #region Private Helper Methods

        private void FillCells(int cellX, int cellY, int newCellIndex, int oldCellIndex)
        {
            if (m_currentLayer.GetCellIndex(cellX, cellY) == oldCellIndex)
                m_currentLayer.SetCellIndex(cellX, cellY, newCellIndex);
            else
                return;

            //recursively go to each of cell neighbours and apply changes if necessary

            //left
            if (cellX > 0)
                FillCells(cellX - 1, cellY, newCellIndex, oldCellIndex);
            //top
            if (cellY > 0)
                FillCells(cellX, cellY - 1, newCellIndex, oldCellIndex);
            //right
            if (cellX < m_currentLayer.LayoutWidth - 1)
                FillCells(cellX + 1, cellY, newCellIndex, oldCellIndex);
            //bottom
            if (cellY < m_currentLayer.LayoutHeight - 1)
                FillCells(cellX, cellY + 1, newCellIndex, oldCellIndex);
        }

        //return the layer file filter
        public static string TileLayerFileFilter { get { return FileHandling.LayerFileHandler.LayerFileFilter; } }

        #endregion
    }
}
