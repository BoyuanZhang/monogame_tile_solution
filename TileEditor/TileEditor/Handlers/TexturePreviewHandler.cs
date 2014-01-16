using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using TileEditor.Utility;

namespace TileEditor.Handlers
{
    class TexturePreviewHandler : Handler
    {
        //dictionary of images, key: trimmed file name, value: image
        Dictionary<string, Image> m_imageDictionary;

        public TexturePreviewHandler() 
        {
            m_imageDictionary = new Dictionary<string, Image>();
        } 

        public override bool HandleFile(string fileName)
        {
            try
            {
                Image image = Image.FromFile(fileName);
                string trimmedFilePath = FileUtility.GetFileNameWithParentFolder(fileName);

                if( !m_imageDictionary.ContainsKey( trimmedFilePath ) )
                    m_imageDictionary.Add( trimmedFilePath, image );

                return true;
            }
            catch (Exception e)
            {
                //Usually if file doesn't exist. For now just return false so the user is informed there is no preview for this texture.
                //In the future maybe we can give more information to the user
                System.Console.WriteLine(e.Message);
                return false;
            }
        }

        public Image GetImage(string imageKey)
        {
            if (m_imageDictionary.ContainsKey(imageKey))
            {
                return m_imageDictionary[imageKey];
            }

            //could not be found in dictionary so return null value
            return null;
        }

        public void Remove(string imageKey)
        {
            //Remove if it exists, otherwise do nothing
            if (m_imageDictionary.ContainsKey(imageKey))
                m_imageDictionary.Remove(imageKey);
        }
    }
}
