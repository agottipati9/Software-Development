﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Rogue_Game
{
    class Game
    {
        // The screen height and width are in number of tiles
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;
        private static RLRootConsole _rootConsole;

        // The map console takes up most of the screen and is where the map will be drawn
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;
        private static RLConsole _mapConsole;

        // Below the map the console is the inventory console which shows the players equipment, abilities, and items
        private static readonly int _inventoryWidth = 80;
        private static readonly int _inventoryHeight = 11;
        private static RLConsole _inventoryConsole;

        // The stat console is to the right of the map and display player and monster stats
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;
        private static RLConsole _statConsole;

        // Above the map is the message console which displays attack rolls and other information
        private static readonly int _messageWidth = 80;
        private static readonly int _messageHeight = 11;
        private static RLConsole _messageConsole;


        static void Main(string[] args)
        {
            string fontFileName = "terminal8x8.png";
            // Title at the top of the console window
            string consoleTitle = "Rouge Sharp V3 Tutorial - Level 1";
            // Tell RLNet to use the bitmap and specify that each tile is 8 x 8
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
                8, 8, 1f, consoleTitle);

            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _inventoryConsole = new RLConsole(_inventoryWidth, _inventoryHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _messageConsole = new RLConsole(_messageWidth, _messageHeight);

            // Set up a handler for RLNET's Update Event
            _rootConsole.Update += OnRootConsoleUpdate;
            // Set up a handler for RLNET's Render Event
            _rootConsole.Render += OnRootConsoleRender;
            // Begin RLNET's Game Loop
            _rootConsole.Run();
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            //Set background color and text for each console
            // so that we can verify they are in the correct positions
            _mapConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, RLColor.Black);
            _mapConsole.Print(1, 1, "Map", RLColor.White);

            _inventoryConsole.SetBackColor(0, 0, _inventoryWidth, _inventoryHeight, RLColor.Cyan);
            _inventoryConsole.Print(1, 1, "Inventory", RLColor.White);

            _statConsole.SetBackColor(0, 0, _statWidth, _statHeight, RLColor.Brown);
            _statConsole.Print(1, 1, "Stats", RLColor.White);

            _messageConsole.SetBackColor(0, 0, _messageWidth, _messageHeight, RLColor.Gray);
            _messageConsole.Print(1, 1, "Messages", RLColor.White);
        }

        //Event handler for RLNET's Render Event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            //Blit the sub consoles to the root console in the correct locations
            RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight,
                _rootConsole, 0, _inventoryHeight);
            RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight,
                _rootConsole, _mapWidth, 0);
            RLConsole.Blit(_inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight,
                _rootConsole, 0, _screenHeight - _inventoryHeight);
            RLConsole.Blit(_messageConsole, 0, 0, _messageWidth, _messageHeight,
                _rootConsole, 0, 0);

            // Tell RLNET to draw the console
            _rootConsole.Draw();
        }
    }
}
