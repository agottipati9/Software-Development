using System;
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

        static void Main(string[] args)
        {
            string fontFileName = "terminal8x8.png";
            // Title at the top of the console window
            string consoleTitle = "Rouge Sharp V3 Tutorial - Level 1";
            // Tell RLNet to use the bitmap and specify that each tile is 8 x 8
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
                8, 8, 1f, consoleTitle);

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
            _rootConsole.Print(10, 10, "It worked!", RLColor.White);
        }

        //Event handler for RLNET's Render Event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            // Tell RLNET to draw the console
            _rootConsole.Draw();
        }
    }
}
