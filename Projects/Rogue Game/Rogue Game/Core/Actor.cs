﻿using RLNET;
using Rogue_Game.Interfaces;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Game.Core
{
    public class Actor : IActor, IDrawable
    {
        //IActor
        public string Name { get; set; }
        public int Awareness { get; set; }

        // IDrawable
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(RLConsole console, IMap map)
        {
            // Don't draw actors in cells that haven't been explored
            if (!map.GetCell(X, Y).IsExplored)
                return;

            // Only draw the actor witht the color and symbol when they are in the field of view
            if (map.IsInFov(X, Y))
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            // When not in the field of view just draw a norm floor 
            else
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
                
        }

    }
}
