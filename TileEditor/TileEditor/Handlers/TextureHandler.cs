using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TileEditor.Utility;

namespace TileEditor.Handlers
{
    public class TextureHandler : Handler
    {
        //Graphics device to handle graphical data (textures)
        private GraphicsDevice m_graphicsDevice;

        //Dictionary of textures Key: parsed filename, value: texture
        private Dictionary<string, Texture2D> m_textureDictionary;

        public TextureHandler( GraphicsDevice graphicsDevice)
        {
            m_graphicsDevice = graphicsDevice;

            m_textureDictionary = new Dictionary<string, Texture2D>();
        }

        public override bool HandleFile(string textureFileName)
        {
            try
            {
                Texture2D texture = Texture2D.FromStream(m_graphicsDevice, File.OpenRead(textureFileName));

                //Check if trimmed file name is in the dictionary, if it is return false (not handled), otherwise add to dictionary and return true
                string trimmedFileName = FileUtility.GetFileNameWithParentFolder(textureFileName);

                if (!m_textureDictionary.ContainsKey(trimmedFileName))
                {
                    m_textureDictionary.Add(trimmedFileName, texture);
                    return true;
                }

                //handler already contains this texture!
                return false;
            }
            catch( Exception e)
            {
                //Usually if a file doesn't exist...
                //throw an exception to the manager 
                throw e;
            }
        }

        public void Remove(string textureKey)
        {
            //Remove if exists, otherwise do nothing
            if (m_textureDictionary.ContainsKey(textureKey))
                m_textureDictionary.Remove(textureKey);
        }

        public bool ContainsTexture(string textureKey)
        {
            if (m_textureDictionary.ContainsKey(textureKey))
                return true;

            return false;
        }

        public Texture2D GetTexture(string textureKey)
        {
            if (m_textureDictionary.ContainsKey(textureKey))
                return m_textureDictionary[textureKey];

            return null;
        }
    }
}
