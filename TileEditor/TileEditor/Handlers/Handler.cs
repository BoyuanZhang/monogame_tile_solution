using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace TileEditor.Handlers
{
    public abstract class Handler
    {
        //Base handler.. I wanted to divide up the logic, but the only thing these handlers seem to have in common is the ability
        //to handle a file for open / loading, and a data dictionary.
        public Handler()
        {

        }

        public abstract bool HandleFile(string fileName);
    }
}
