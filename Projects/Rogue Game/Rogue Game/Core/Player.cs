using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Game.Core
{
    public class Player : Actor
    {
        public Player()
        {
            Awareness = 15;
            Name = "Vader";
            Color = Colors.Player;
            Symbol = '+';
            X = 10;
            Y = 10;
        }
    }
}
