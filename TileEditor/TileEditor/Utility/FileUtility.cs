using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TileEditor.Utility
{
    public static class FileUtility
    {
        public static string GetFileNameWithParentFolder(string fileName)
        {
            return Directory.GetParent(fileName).Name + "\\" + Path.GetFileNameWithoutExtension(fileName);
        }
    }
}
