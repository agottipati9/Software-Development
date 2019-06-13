using RLNET;
using RogueSharp;
using System;
using Rogue_Game.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Game.Core
{
    //DungeonMap extends the base RogueSharp Map Class
    public class DungeonMap : Map
    {
        public List<Rectangle> Rooms;
        public static Player Player { get; set; }

        public DungeonMap()
        {
            //Initialize the list of rooms when we create a new DungeonMap
            Rooms = new List<Rectangle>();
        }

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

        // This method will be alled any time we move the player to update field of view
        public void UpdatePlayerFieldOfView()
        {
            Player player = Game.Player;
            bool areLightWalls = true, isExplored = true;

            //Compute the field of view based on the player's location and awareness
            ComputeFov(player.X, player.Y, player.Awareness, areLightWalls);

            //Mark all cells in field of view as having been explored
            foreach(Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, isExplored);
            }
        }

        // Returns true when able to place the Actor on the cell and false otherwise.
        public bool SetActorPosition(Actor actor, int x, int y)
        {
            // Only allow actor placement if the cell is walkable
            if(GetCell(x, y).IsWalkable)
            {
                bool isWalkable = true;
                // The cell the actor was previously on is now walkable
                SetIsWalkable(actor.X, actor.Y, isWalkable);

                // Update actor's position
                actor.X = x;
                actor.Y = y;

                //The new cell the actor is on is now not walkable
                isWalkable = false;
                SetIsWalkable(actor.X, actor.Y, isWalkable);

                // Update field of view since reposition the player
                if (actor is Player)
                    UpdatePlayerFieldOfView();

                return true;
            }

            return false;
        }

       // Sets the IsWalkable property on a Cell
       public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            ICell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        // Adds the player to the map
        public void AddPlayer(Player player)
        {
            bool isWalkable = false;
            Game.Player = player;
            SetIsWalkable(player.X, player.Y, isWalkable);
            UpdatePlayerFieldOfView();
        }
    }
}
