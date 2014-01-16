using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEditor.GLControlSource
{
    //So... really weird windows forms bug that does not allow you to use controls that are abstracting first first level inheritance.
    //To get around this, we implement a "middle abstraction", because controls that abstract from second level inheritance and onwards work.
    public abstract class GraphicsDeviceControlWrapper : GraphicsDeviceControl
    {
    }
}
