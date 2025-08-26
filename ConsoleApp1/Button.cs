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
    public class Button : GUI
    {
        private string text;
        private int fontSize;
        private int fontSpacing = 3;
        private Color defaultTextColor;
        private Color hoverTextColor;
        private Vector2 textSize;
        private Vector2 textPos;
        private Font fontDefault = Raylib.GetFontDefault();
        public Button(int x, int y, int w, int h, Color lineColor, Color defaultColor, Color hoverColor, string text, int fontSize, Color defaultTextColor, Color hoverTextColor) : base(x, y, w, h, lineColor, defaultColor, hoverColor)
        {
            this.text = text;
            this.fontSize = fontSize;
            this.defaultTextColor = defaultTextColor;
            this.hoverTextColor = hoverTextColor;
            textSize = Raylib.MeasureTextEx(fontDefault, text, fontSize, fontSpacing);
            textPos.X = x + w * 0.5f - textSize.X * 0.5f;
            textPos.Y = y + h * 0.5f - textSize.Y * 0.5f;
        }

        public void ButtonDraw()
        {

            if (isVisible)
            {
                if (!isHover)
                {
                    Raylib.DrawRectangle(x, y, w, h, defaultColor);
                    Raylib.DrawRectangleLines(x, y, w, h, lineColor);
                    Raylib.DrawTextEx(fontDefault, text, textPos, fontSize, fontSpacing, defaultTextColor);
                }
                else
                {
                    Raylib.DrawRectangle(x, y, w, h, hoverColor);
                    Raylib.DrawRectangleLines(x, y, w, h, lineColor);
                    Raylib.DrawTextEx(fontDefault, text, textPos, fontSize, fontSpacing, hoverTextColor);
                }
                
            }
        }

    }


}
