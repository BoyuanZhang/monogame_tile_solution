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

            //hard-coded in path ending for now... come up with a better solution for finding the outline texture afterwards
            EditorOutlineTexture = Texture2D.FromStream(graphicsDevice, File.OpenRead( contentPath + "/" + "TileEditor/tile_outline.png"));
        }

        //take in parameters as string and attempt to parse them, or put defaults in if input not in correct format
        //return false if layer could not be handled (layer name already exists etc...)
        public bool HandleNewLayer(string layerName, string layerWidth, string layerHeight)
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
            if (m_tileLayerHandler.HandleNewLayer(layerName, width, height))
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
                return "";
            }
            catch (Exception e)
            {
                //Data manager will log exception and return an empty string to signal a failed handle of the texture
                System.Console.WriteLine( e.Message );
                //Should probably call client Error / Exception / Warning window class to display a proper message to user

                //return empty string
                return "";
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
        public KeyValuePair<int, int> GetLayerDimensions( List<string> layerNames)
        {
            return m_tileLayerHandler.GetLayerDimensions(layerNames);
        }

        public int GetLayerAlpha(string layerName)
        {
            return m_tileLayerHandler.GetLayerAlpha(layerName);
        }

        //Not sure if this is good design, but here we are allowing the data manager to offer up handles to each of it's handlers
        public TileLayerHandler TileLayerDataHandle { get { return m_tileLayerHandler; } }
    }
}
