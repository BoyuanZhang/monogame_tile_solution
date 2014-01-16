using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TileEngine.Layer;

namespace TileEngine.Map
{
    class TileMap
    {
        private List<TileLayer> m_tileLayerList;

        public TileMap()
        {
            m_tileLayerList = new List<TileLayer>();
        }
    }
}
