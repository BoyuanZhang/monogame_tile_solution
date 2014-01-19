using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework.Graphics;

using TileEngine.Layer;
using TileEngine.Layer.DataObjects;
using TileEngine.Layer.CreationObjects;

namespace TileEngine.Filing
{
    public static class EngineFileHandler
    {
        //Supported layer formats
        public static string LayerFileFilter = "Layer File|*.layer";
        //Supported extensions
        private static string[] imageExtensions = new string[] 
                {
                    ".jpg", ".png", ".gif"
                };

        public static void SaveLayer(string contentPath, string newFileName, EditorTileLayer tileLayer, List<string>textureFileNames)
        {
            //How layer saving will work is, we need to keep in mind when saving, when loading files in they must be loaded in a
            //particular order. Saving must obey this order.
            //Currently its Texturelist, Property list, tile layout
            try
            {
                using (StreamWriter writer = new StreamWriter(newFileName))
                {
                    writer.WriteLine("[Textures]");
                    foreach (string textureName in textureFileNames)
                    {
                        string fullTexturePath = contentPath + "\\" + textureName;
                        //save the texture, only if it exists currently in the content path
                        string fullPathWithExtension = string.Empty;

                        foreach (string ext in imageExtensions)
                        {
                            if (File.Exists(fullTexturePath + ext))
                            {
                                fullPathWithExtension = fullTexturePath + ext;
                                break;
                            }
                        }

                        if( !string.IsNullOrEmpty( fullPathWithExtension ) )
                            writer.WriteLine(fullPathWithExtension);
                    }
                    writer.WriteLine();
                    
                    //Write properties
                    writer.WriteLine("[Properties]");
                    //alpha channel
                    writer.WriteLine("Alpha = " + tileLayer.Alpha.ToString() );
                    //layer Type
                    writer.WriteLine("Layer Type Index = " + ((int)tileLayer.LayerLayoutType).ToString() );

                    writer.WriteLine();
                    //Layout
                    writer.WriteLine("[Layout]");
                    writer.WriteLine();

                    for (int x = 0; x < tileLayer.LayoutWidth; x++)
                    {
                        string layerRow = string.Empty;
                        for (int y = 0; y < tileLayer.LayoutHeight; y++)
                        {
                            layerRow += tileLayer.GetCellIndex(x, y).ToString() + " , ";
                        }
                        writer.WriteLine(layerRow);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Try to open and read from a selected layer
        public static EditorTileLayerDO OpenEditorLayer(string layerFileName)
        {
            try
            {
                TileLayerCO layerCreationObject = ProcessLayer( layerFileName );
                //Validate this creation object (not yet implemented)
                if( ValidateCreationObject( layerCreationObject ) )
                {
                    //Loop through properties of the layer (alpha channel etc.. ), and store them to initiate/set values in the layer
                    //Property values: alpha channel and layer type - default being normal
                    float alphaValue = 0.0f;
                    LayerType.LayerTypesEnum layerType = LayerType.LayerTypesEnum.Normal;

                    foreach (KeyValuePair<string, string> propertyPair in layerCreationObject.LayerPropertyDictionary)
                    {
                        //handle all properties in switch
                        switch (propertyPair.Key.Trim().ToLower())
                        {
                            case "alpha":
                                alphaValue = float.Parse(propertyPair.Value);
                                break;
                            case "layer type index":
                                layerType = (LayerType.LayerTypesEnum)(int.Parse( propertyPair.Value ) );
                                break;
                            default:
                                break;
                        }
                    }

                    //Create new editor tile layer
                    EditorTileLayer editorTileLayer = new EditorTileLayer( layerCreationObject.LayerWidth, layerCreationObject.LayerHeight, layerType );

                    //Set all properties of tile layer, and intialize all editor tiles within this editor layer
                    for (int x = 0; x < layerCreationObject.LayerWidth; x++)
                        for (int y = 0; y < layerCreationObject.LayerHeight; y++)
                            editorTileLayer.SetCellIndex( x, y, layerCreationObject.LayerIndexLayout[x][y]);

                    //Set the return data object, to return the initialized editor tile layer, and the texture names, so the necessary textures
                    //can be added to managed data, and added into the layer's texture list.
                    EditorTileLayerDO editorLayerDO = new EditorTileLayerDO( editorTileLayer, layerCreationObject.LayerTextureNameList );

                    return editorLayerDO;
                }
                else
                    return null;
            }
            catch( Exception e )
            {
                throw e;
            }
        }

        private static bool ValidateCreationObject( TileLayerCO creationObject )
        {
            //not implemented yet, but this will be used to verify all creation objects 
            //if the creation object is not valid, return null
            return true;
        }

        //Read and parse selected layer file
        private static TileLayerCO ProcessLayer(string layerFileName)
        {
            try
            {
                //Instantiate the creation object to hold all creation data of this tile layer
                TileLayerCO tileLayerCO = new TileLayerCO();
                //layer Layout list of index to be read in, in the future instead of an integer index, it will probably be an object
                using (StreamReader streamReader = new StreamReader(layerFileName))
                {
                    bool readingTextures = false;
                    bool readingProperties = false;
                    bool readingLayout = false;

                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine().Trim();

                        if (!string.IsNullOrEmpty(line))
                        {
                            if (line.Contains("[Textures]"))
                            {
                                readingTextures = true;
                                readingProperties = false;
                                readingLayout = false;
                            }
                            else if (line.Contains("[Properties]"))
                            {
                                readingTextures = false;
                                readingProperties = true;
                                readingLayout = false; ;
                            }
                            else if (line.Contains("[Layout]"))
                            {
                                readingTextures = false;
                                readingProperties = false;
                                readingLayout = true;
                            }
                            else if (readingTextures)
                            {
                                tileLayerCO.LayerTextureNameList.Add(line);
                            }
                            else if (readingProperties)
                            {
                                string[] propertyPairs = line.Split('=');
                                //There should always be an index 0 / and 1 for the property pairs. Probably should add a try-catch here to handle when this isn't the case
                                string key = propertyPairs[0].Trim();
                                string value = propertyPairs[1].Trim();

                                tileLayerCO.LayerPropertyDictionary.Add(key, value);
                            }
                            else if (readingLayout)
                            {
                                List<int> row = new List<int>();

                                String[] textureIndexes = line.Split(',');

                                foreach (String textureIndex in textureIndexes)
                                {
                                    //Should be trimmed but this is a backup
                                    if (!String.IsNullOrEmpty(textureIndex))
                                        row.Add(Convert.ToInt32(textureIndex.Trim()));
                                }

                                tileLayerCO.LayerIndexLayout.Add(row);
                            }
                        }
                    }
                }

                return tileLayerCO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
