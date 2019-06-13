﻿using System;
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
        private readonly int _width;
        private readonly int _height; 
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;

        private readonly DungeonMap _map;

        // Constructs a new MapGenerator based on the given width and height.
        public MapGenerator(int width, int height, int maxRooms, 
            int roomMaxSize, int roomMinSize)
        {
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _map = new DungeonMap();
        }

        //Generate a new map that is asimple open floor with walls around the outside.
        public DungeonMap CreateMap()
        {
            // Intialize every cell in the map by setting
            // walkable, transparency, and explored to true

            Boolean isWalkable = true, isTransparent = true, hasExplored = true;
            Boolean notWalkable = false, notTransparent = false;

            _map.Initialize(_width, _height);
            
            // Place as many rooms as the specified maxRooms
            for(int r = _maxRooms; r > 0; r--)
            {
                // Determine the size and position of the room randomly
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomXPosition = Game.Random.Next(0, _width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, _height - roomHeight - 1);

                // All our rooms are represented as Retangles
                var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);

                // Check to see if the rooms intersect with each other
                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));

                //If room doesn't intersect add it to the list of rooms
                if (!newRoomIntersects)
                    _map.Rooms.Add(newRoom);

                foreach (Rectangle room in _map.Rooms)
                    CreateRoom(room);
            }

            return _map;
        }

        //Sets the cell properties for the area to be true based on the room's area
        private void CreateRoom(Rectangle room)
        {
            bool isWalkable = true, isTransparent = true, hasExplored = true;

            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for(int y = room.Top + 1; y < room.Bottom; y++)
                {
                    _map.SetCellProperties(x, y, isTransparent, isWalkable, hasExplored);
                }
            }
        }

    }
}
