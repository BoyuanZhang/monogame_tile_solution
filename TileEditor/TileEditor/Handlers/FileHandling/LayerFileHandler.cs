using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using TileEditor.Utility;

using TileEngine.Filing;
using TileEngine.Layer;
using TileEngine.Layer.DataObjects;

using Microsoft.Xna.Framework.Graphics;

namespace TileEditor.Handlers.FileHandling
{
    public static class LayerFileHandler
    {
        public static string LayerFileFilter { get { return EngineFileHandler.LayerFileFilter; } }

        public static void SaveLayer( string contentPath, string newFileName, EditorTileLayer tileLayer, List<string>textureFileNames )
        {
            try
            {
                EngineFileHandler.SaveLayer( contentPath, newFileName, tileLayer, textureFileNames);
            }
            catch( Exception e )
            {
                throw e;
            }
        }

        //return an editor tile layer data object, which will hold the necessary data for processing on the handler / manager layers
        public static EditorTileLayerDO OpenEditorLayer(string fileName, TextureHandler textureHandler, TexturePreviewHandler texturePreviewHandler)
        {
            try
            {
                EditorTileLayerDO openedTileLayerDO = EngineFileHandler.OpenEditorLayer(fileName);

                //loop through each texture name, load the texture and add it to the layer. 
                //We need to decide what to do when textures have been deleted / no longer exist in that directory.. should we not load the level at all?
                //Or maybe have each tile hold a reference to the texture file, and just not draw the textures who's files have not been loaded in
                List<string> modifiedTextureFileNames = new List<string>();
                foreach (string textureFile in openedTileLayerDO.LayerTextureNameList)
                {
                    string trimmedTextureFile = FileUtility.GetFileNameWithParentFolder(textureFile);
                    if (textureHandler.HandleFile(textureFile))
                    {
                        texturePreviewHandler.HandleFile(textureFile);
                        //we just successfully handled that texture, it should be stored in memory now
                        openedTileLayerDO.EditorTileLayer.AddNewTexture(textureHandler.GetTexture(trimmedTextureFile));
                        //now add it to our modified texture file names, so we can pass it back to the client for display
                        modifiedTextureFileNames.Add(trimmedTextureFile);
                    }
                    else
                    {
                        //The texture still exists in memory, just add it to the tile layers texture list. It will be added in the correct index
                        //just make sure the texture handler contains it...
                        if (textureHandler.ContainsTexture(trimmedTextureFile))
                            openedTileLayerDO.EditorTileLayer.AddNewTexture(textureHandler.GetTexture(trimmedTextureFile));
                    }
                }

                openedTileLayerDO.LayerTextureNameList.Clear();
                foreach (string modifiedFile in modifiedTextureFileNames)
                {
                    openedTileLayerDO.LayerTextureNameList.Add(modifiedFile);
                }
                //return new Tuple with all texture that are handled correctly, and have had their file names modified

                return openedTileLayerDO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
