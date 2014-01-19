using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace TileEngine.Layer.Tiles
{
    public class EditorTile : Tile
    {
        //Editor does not use traditional camera, so for drawing it offsets the current position of the screen from the tiles position
        private Point m_tilePosition;

        public EditorTile(int textureIndex, Point position) : base( textureIndex )
        {
            m_tilePosition = position;
        }

        public Point TilePosition { get { return m_tilePosition; } }
    }
}
