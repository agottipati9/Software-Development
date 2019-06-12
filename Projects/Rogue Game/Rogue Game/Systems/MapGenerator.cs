using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue_Game.Core;
using RogueSharp;

namespace Rogue_Game.Systems
{
    class MapGenerator
    {
        /** The width of the map */
        private readonly int _width;
        /** The height of the map */
        private readonly int _height;
        /** Represents the map */
        private readonly DungeonMap _map;

        // Constructs a new MapGenerator based on the given width and height.
        public MapGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            _map = new DungeonMap();
        }

        //Generate a new map that is asimple open floor with walls around the outside.
        public DungeonMap CreateMap()
        {
            // Intialize every cell in the map by setting
            // walkable, transparency, and explored to true

            Boolean isWalkable = true, isTransparent = true, hasExplored = true;
            Boolean notWalkable = false, notTransparent = false, notExplored = false;

            _map.Initialize(_width, _height);
            foreach(Cell cell in _map.GetAllCells())
            {
                _map.SetCellProperties(cell.X, cell.Y, isTransparent, isWalkable, hasExplored);
            }

            //Set the first and last rows in the map to not be transparent or walkable
            foreach(Cell cell in _map.GetCellsInRows(0, _height - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, notWalkable, notTransparent, hasExplored);
            }

            //Set the first and last columns in the map to not be transparent or walkable
            foreach(Cell cell in _map.GetCellsInColumns(0, _width - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, notWalkable, notTransparent, hasExplored);
            }

            return _map;
        }

    }
}
