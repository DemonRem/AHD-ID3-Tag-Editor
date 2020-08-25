using System;
using System.Drawing;

namespace AHD.ID3.Editor
{ 
    [Serializable()]
    public class ToolBar
    {
        private string name = "Main";
        private ToolBarType type = ToolBarType.Main;
        private ToolBarLocation location = ToolBarLocation.Top;
        private Point position = new Point(0, 0);
        private bool visible = true;

        public string Name
        { get { return name; } set { name = value; } }
        public ToolBarType Type
        { get { return type; } set { type = value; } }
        public ToolBarLocation Location
        { get { return location; } set { location = value; } }
        public Point Position
        { get { return position; } set { position = value; } }
        public bool Visible
        { get { return visible; } set { visible = value; } }
    }
}
