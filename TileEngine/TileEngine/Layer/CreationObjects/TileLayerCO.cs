using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TileEngine;

namespace TileEngine.Layer.CreationObjects
{
    //Object used to hold all information necessary for the creation of Tile Layers
    public class TileLayerCO
    {
        private LayerType.LayerTypesEnum m_layerType;
        private List<List<int>> m_layerIndexLayout;

        private Dictionary<string, string> m_layerPropertyDictionary;
        private List<string> m_layerTextureNameList;

        public TileLayerCO()
        {
            m_layerIndexLayout = new List<List<int>>();

            m_layerPropertyDictionary = new Dictionary<string, string>();
            m_layerTextureNameList = new List<string>();
        }

        public int LayerWidth { get { return m_layerIndexLayout[0].Count; } }

        public int LayerHeight { get { return m_layerIndexLayout.Count; } }

        public List<List<int>> LayerIndexLayout { get { return m_layerIndexLayout; } }

        public Dictionary<string, string> LayerPropertyDictionary { get { return m_layerPropertyDictionary; } }

        public List<string> LayerTextureNameList { get { return m_layerTextureNameList; } }

        public LayerType.LayerTypesEnum LayerType { get { return m_layerType; } set { m_layerType = value; } }
    }
}
