using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TileEngine.Layer;

namespace TileEngine.Layer.DataObjects
{
    public class EditorTileLayerDO
    {
        private EditorTileLayer m_tileLayer;
        private List<string> m_layerTextureNameList;

        public EditorTileLayerDO(EditorTileLayer tileLayer, List<string> textureNameList)
        {
            m_tileLayer = tileLayer;
            m_layerTextureNameList = textureNameList;
        }

        public EditorTileLayer EditorTileLayer { get { return m_tileLayer; } }
        public List<string> LayerTextureNameList { get { return m_layerTextureNameList; } }
    }
}
