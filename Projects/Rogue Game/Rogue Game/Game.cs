using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Rogue_Game.Core;
using Rogue_Game.Systems;
using RogueSharp.Random;

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

        public static DungeonMap DungeonMap { get; private set; }
        public static Player Player { get; set; }
        // Random Number Generator used throughout the game
        public static IRandom Random { get; private set; }

        private static bool _renderRequired = true;
        public static CommandSystem CommandSystem { get; private set; }

        static void Main(string[] args)
        {
            // Establish the sedd for the random number generator from the current time
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);

            // The tile will appear at the top of the console, with the seed used to generate the level
            string consoleTitle = $"Level 1 - Seed {seed}";

            string fontFileName = "terminal8x8.png";
            // Tell RLNet to use the bitmap and specify that each tile is 8 x 8
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
                8, 8, 1f, consoleTitle);

            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _inventoryConsole = new RLConsole(_inventoryWidth, _inventoryHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _messageConsole = new RLConsole(_messageWidth, _messageHeight);

            //Creates the Dungeon Map
            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapWidth, 20, 13, 7);
            DungeonMap = mapGenerator.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();

            // Commands for player movement
            CommandSystem = new CommandSystem();

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

            _inventoryConsole.SetBackColor(0, 0, _inventoryWidth, _inventoryHeight, Swatch.DbDeepWater);
            _inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);

            _statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Swatch.DbWood);
            _statConsole.Print(1, 1, "Stats", Colors.TextHeading);

            _messageConsole.SetBackColor(0, 0, _messageWidth, _messageHeight, Swatch.DbOldStone);
            _messageConsole.Print(1, 1, "Messages", Colors.TextHeading);

            bool didPlayerAct = false;
            RLKeyPress keyPress = _rootConsole.Keyboard.GetKeyPress();

            if(keyPress != null)
            {
                if(keyPress.Key == RLKey.Up)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
                }
                else if (keyPress.Key == RLKey.Down)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Down);
                }
                else if (keyPress.Key == RLKey.Left)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
                }
                else if (keyPress.Key == RLKey.Right)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Right);
                }
                else if (keyPress.Key == RLKey.Escape)
                {
                    _rootConsole.Close();
                }
            }

            if (didPlayerAct)
                _renderRequired = true;
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

            if (_renderRequired)
            {
                // Draws the Dungeon Map
                DungeonMap.Draw(_mapConsole);

                // Draws the player
                Player.Draw(_mapConsole, DungeonMap);

                _renderRequired = false;
            }
            
            
        }
    }
}
