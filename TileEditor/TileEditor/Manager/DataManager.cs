using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

using Microsoft.Xna.Framework.Graphics;

using TileEditor.Handlers;
using TileEditor.Utility;

namespace TileEditor.Manager
{
    public class DataManager
    {
        //Different handlers for the different data that the form will contain (textures, images, layer files etc... )
        private TextureHandler m_textureHandler;
        private TileLayerHandler m_tileLayerHandler;
        private TexturePreviewHandler m_texturePreviewHandler;

        //outline texture for the editor
        public Texture2D EditorOutlineTexture;

        public DataManager( GraphicsDevice graphicsDevice, string contentPath ) 
        {
            m_textureHandler = new TextureHandler(graphicsDevice);
            m_tileLayerHandler = new TileLayerHandler();
            m_texturePreviewHandler = new TexturePreviewHandler();

            //initialize any content the data manager will need (textures etc...)
            InitializeContent( graphicsDevice, contentPath);
        }

        private void InitializeContent( GraphicsDevice graphicsDevice, string contentPath)
        {
            //hard-coded in path ending for now... come up with a better solution for finding the outline texture afterwards
            //Try to get the outlining texture from the content / tileeditor folder.
            try
            {
                EditorOutlineTexture = Texture2D.FromStream(graphicsDevice, File.OpenRead(contentPath + "/" + "TileEditor/tile_outline.png"));
            }
            catch (Exception e)
            {
                //log error
                System.Console.WriteLine(e.Message);
                //Should pass this to a Form message box handler in the future instead of letting the data manager handle it
                System.Windows.Forms.MessageBox.Show(e.Message + " Tile outlining will not be enabled",
                                                       "Warning",
                                                       System.Windows.Forms.MessageBoxButtons.OK,
                                                       System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        //take in parameters as string and attempt to parse them, or put defaults in if input not in correct format
        //return false if layer could not be handled (layer name already exists etc...)
        public bool HandleNewLayer(string layerName, string layerWidth, string layerHeight, int layerType)
        {
            //Parse parameters before giving to layer handler
            int width = 0;
            int height = 0;
            try
            {
                width = int.Parse(layerWidth);
                height = int.Parse(layerHeight);

                //Setting the max / minimum map parameters
                if (width > 0)
                    width = Math.Min(width, 80);
                else
                    width = 10;

                if (height > 0)
                    height = Math.Min(height, 80);
                else
                    height = 10;
            }
            catch (FormatException)
            {
                //Defaults to width of 10 and height of 10 if an invalid values are input
                width = 10;
                height = 10;
            }

            //If the new layer is correctly added into data management then return true
            if (m_tileLayerHandler.HandleNewLayer(layerName, width, height, layerType))
                return true;

            return false;
        }

        public void RemoveSelectedLayers(List<string> layerNames)
        {
            foreach (string name in layerNames)
            {
                m_tileLayerHandler.RemoveLayer(name);
            }
        }

        //Try to open an existing layer
        //and return a tuple of data, containing the trimmed file name, and list of textures names to be handled that are loaded in with the layer
        public Tuple<string, List<string>> OpenLayer(string layerFileName)
        {
            try
            {
                //Tuple returned after all file handling is completed, including texture name trimming
                Tuple<string, List<string> > layerTuple = m_tileLayerHandler.HandleExistingLayer(layerFileName, m_textureHandler, m_texturePreviewHandler);

                //Only process if the raw tuple contains a valid layer name

                if (!string.IsNullOrEmpty(layerTuple.Item1))
                {
                    return new Tuple<string, List<string>>(layerTuple.Item1, layerTuple.Item2);
                }

                return new Tuple<string, List<string>>(string.Empty, null);
            }
            catch (Exception e)
            {
                //log error
                System.Console.WriteLine(e.Message);
                //Prompt error to user, stating that the layer file could not be opened, and return an empty tuple (empty fileName, null texture name list)
                return new Tuple<string, List<string>>(string.Empty, null);
            }
        }

        //Save current selected layer
        public void SaveLayer(string contentPath, string saveName, string layerName, List<string> textureFileNames )
        {
            try
            {
                if( m_tileLayerHandler.ContainsLayer( layerName ) )
                {
                    //Get complete texture file list that the layer is using, then get it's corresponding index in the layer's texture list,
                    //and add them in order into a new texture file list to be passed on to be saved
                    Dictionary<int, string> sortedLayerTextureMap = new Dictionary<int, string>();
                    foreach (string textureFile in textureFileNames)
                    {
                        if (m_textureHandler.ContainsTexture(textureFile))
                        {
                            int index = m_tileLayerHandler.GetLayer(layerName).UsedTextureIndex(m_textureHandler.GetTexture(textureFile));
                            //if the layer is using this texture, then add it to the sorted layer
                            if (index >= 0)
                            {
                                sortedLayerTextureMap.Add(index, textureFile);
                            }
                        }
                    }
                    //sweet, now get all textures in the sortedLayerTextureMap into a list, and pass it over to the tile layer handler to save
                    List<string> usedTextureNameList = new List<string>();
                    for (int i = 0; i < sortedLayerTextureMap.Count; i++)
                    {
                        usedTextureNameList.Add(sortedLayerTextureMap[i]);
                    }
                    //Pass complete texture list to the layer handler's save, so that it may check which ones the layer being saved is using
                    m_tileLayerHandler.SaveLayer(contentPath, saveName, layerName, usedTextureNameList);
                }
            }
            catch (Exception e)
            {
                //Log error
                System.Console.WriteLine(e.Message);
            }
        }

        //Layer editing handlers

        //Edit draw data
        public void EditLayerDataDraw(bool fill, string textureKey)
        {
            //Let texture handler see if this texture exists, if it does get it and pass it to the layer handler to update current layout
            if( m_textureHandler.ContainsTexture( textureKey ))
                m_tileLayerHandler.DrawToLayer(fill, m_textureHandler.GetTexture( textureKey) );
        }

        //Erase draw data
        public void EditLayerDataErase(bool fill)
        {
            m_tileLayerHandler.EraseFromLayer(fill);
        }

        //change alpha channel of top most selected layer
        public void ChangeLayerAlpha(float alpha)
        {
            m_tileLayerHandler.SetAlpha(alpha);
        }

        public string HandleTexture(string textureFileName)
        {
            //Call texture handler to manage the texture... should probably either validate here or the form that the texture is in the right path.
            try
            {
                if (m_textureHandler.HandleFile(textureFileName))
                {
                    //If texture handler is successful then add the image file to the preview data, if it is unable to be added
                    //warn the user that there will be no preview available for this texture
                    if (!m_texturePreviewHandler.HandleFile(textureFileName))
                    {
                        //TODO: Warn user preview not available for this texture
                    }
                    //After handling the texture return the trimmed string of the fileName (parent directory / file name without extension) to add to the form texture list box
                    return FileUtility.GetFileNameWithParentFolder(textureFileName);
                }
                //If not handled return empty string
                return string.Empty;
            }
            catch (Exception e)
            {
                //Data manager will log exception and return an empty string to signal a failed handle of the texture
                System.Console.WriteLine( e.Message );
                //Should probably call client Error / Exception / Warning window class to display a proper message to user

                //return empty string
                return string.Empty;
            }
        }

        public void LayerSelectionChanged(string layerName)
        {
            m_tileLayerHandler.UpdateCurrentLayer(layerName);
        }

        public void RemoveTexture(string fileNameKey)
        {
            //Before removing it from the texture handler, check all layer layouts, if a layer is utilizing this texture
            //the cells using this texture must be set back to be empty cells, and the texture must be removed from the tile layer texture list
            if( m_textureHandler.ContainsTexture( fileNameKey ) )
                m_tileLayerHandler.RemoveTextureFromLayers( m_textureHandler.GetTexture(fileNameKey));
  
            m_textureHandler.Remove(fileNameKey);
            m_texturePreviewHandler.Remove(fileNameKey);
        }

        public Image GetPreviewImage(string imageFileKey)
        {
            Image image = m_texturePreviewHandler.GetImage(imageFileKey);

            if (image != null)
            {
                return image;
            }
            else
            {
                //log error and return a null value, so the preview box won't display anything
                System.Console.WriteLine("The key " + imageFileKey + " did not match any image values in storage.");

                return null;
            }
        }
        //Getters of general layer data 

        //Get general layer data for the largest selected layer
        public Tuple<int, int> GetLayerDimensions( List<string> layerNames)
        {
            return m_tileLayerHandler.GetLayerDimensions(layerNames);
        }

        public int GetLayerAlpha(string layerName)
        {
            return m_tileLayerHandler.GetLayerAlpha(layerName);
        }

        public string GetLayerTypeAsString(string layerName)
        {
            return m_tileLayerHandler.GetLayerTypeAsString(layerName);
        }

        //Not sure if this is good design, but here we are allowing the data manager to offer up handles to each of it's handlers
        public TileLayerHandler TileLayerDataHandle { get { return m_tileLayerHandler; } }

        //Gets the tile layer file filter retrieved the engine
        public static string TileLayerFileFilter { get { return TileLayerHandler.TileLayerFileFilter; } }
    }
}
