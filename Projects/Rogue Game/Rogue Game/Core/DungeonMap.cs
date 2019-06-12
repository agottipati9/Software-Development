using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Game.Core
{
    //DungeonMap extends the base RogueSharp Map Class
    public class DungeonMap : Map
    {

        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
                SetConsoleSymbolForCell(mapConsole, cell);
        }

        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            //If cell hasn't been explored yet, don't draw it.
            if (!cell.IsExplored)
            {
                return;
            }

            //When a cell is currently in the field-of-view it should be drawn with lighter colors
            if(IsInFov(cell.X, cell.Y))
            {
                if (cell.IsWalkable)
                {
                    //Symbol drawn for whether a cell is walkable or not '.' for floor and '#' for walls
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
          
            // When a cell is outside of the field of view it should be drawn in darker colors.
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
        }

    }
}
