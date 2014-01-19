using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TileEngine.Layer;

namespace TileEditor.Forms.EngineHelpers
{
    public static class EngineUtility
    {
        public static List<string> GetLayerTypeList()
        {
            Array layerTypeValues = Enum.GetValues(typeof(LayerType.LayerTypesEnum));

            List<string> layerTypeItems = new List<string>();

            //Set combobox to contain all layer types supported by the tile engine
            //if no values in the enum, which should not happen just default it to be a normal layer type
            //Note** The order the values are added in the combo box is the same index order of the LayerType enum
            if (layerTypeValues.Length > 0)
            {
                foreach (LayerType.LayerTypesEnum type in layerTypeValues)
                {
                    switch (type)
                    {
                        case LayerType.LayerTypesEnum.Collision:
                            layerTypeItems.Add("Collision");
                            break;
                        case LayerType.LayerTypesEnum.Normal:
                            layerTypeItems.Add("Normal");
                            break;
                        default:
                            break;
                    }
                }
            }
            else
                layerTypeItems.Add("Normal");

            return layerTypeItems;
        }

        public static int GetDefaultIndex( List<string> boxItems)
        {
            
            //The combo box will always contain a "Normal" type, so find it and set the index to be where that type is
            //if there isn't a normal type then just return index 0
            int index = 0;
            foreach (string item in boxItems)
            {
                if (item.ToLower() == "normal")
                {
                    //found! Now set that index and leave this loop
                    index = boxItems.IndexOf(item);
                    break;
                }
            }

            return index;
        }
    }
}
