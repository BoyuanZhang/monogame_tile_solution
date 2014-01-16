using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TileEditor.GLControlSource;

namespace TileEditor.Controls
{
    public class GraphicsEditor : GraphicsDeviceControlWrapper
    {
        //public event handler to allow the form to handle requests to update and draw 
        public EventHandler OnDraw;

        private SpriteBatch m_spriteBatch;

        public GraphicsEditor() 
        { 
        } 

        protected override void Initialize()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            GraphicsDevice.Clear(Color.DarkCyan);
        }

        protected override void Draw()
        {
            if (OnDraw != null)
                OnDraw(this, null);
        }

        public SpriteBatch spriteBatch { get { return m_spriteBatch; } }
    }
}
