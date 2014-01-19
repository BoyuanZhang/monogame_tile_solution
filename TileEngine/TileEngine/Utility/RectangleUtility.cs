using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace TileEngine.Utility
{
    //Utility class for rectangle helper methods
    public static class RectangleUtility
    {
        //checks if a rectangle is entirely contained within another
        public static bool ContainedWithin(Rectangle containee, Rectangle container)
        {
            if (containee.Left <= container.Left || containee.Right >= container.Right || containee.Top <= container.Top || containee.Bottom >= container.Bottom)
                return false;
            else
                return true;
        }
    }
}
