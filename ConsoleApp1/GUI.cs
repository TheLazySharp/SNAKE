using Code;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Color = Raylib_cs.Color;
using System.Diagnostics.Metrics;

namespace Code
{
    public abstract class GUI
    {
        int MouseX;
        int MouseY;
        public int x { get; private set; }
        public int y { get; private set; }
        public int w { get; private set; }
        public int h { get; private set; }
        public bool isHover { get; set; }
        public bool isPressed { get; set; }
        public bool isVisible { get; set; }
        public Color lineColor;
        public Color defaultColor;
        public Color hoverColor;

        public GUI(int x, int y, int w, int h, Color lineColor, Color defaultColor, Color hoverColor)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.isHover = false;
            this.isPressed = false;
            this.isVisible = false;
            this.lineColor = lineColor;
            this.defaultColor = defaultColor;
            this.hoverColor = hoverColor;
        }

        public bool MouseHover(GUI visual)
        {
            MouseX = Raylib.GetMouseX();
            MouseY = Raylib.GetMouseY();
            if (MouseX > x && MouseX < x + w && MouseY > y && MouseY < y + h)
            {
                if (!isHover) isHover = true;
                return true;
            }
            else
            {
                if (isHover) isHover = false;
                return false;
            }
        }
    }
}
