using Code;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Color = Raylib_cs.Color;

namespace Code
{

    public class SnakeHead : Snake
    {
        public SnakeHead(string Type, int Column, int Row, Color Color) : base("head", Column, Row)
        {
  
        }
    }



}
