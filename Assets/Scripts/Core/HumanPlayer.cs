using System;

namespace MS.Model
{
    public class HumanPlayer : Player
    {
        public HumanPlayer()
            : base()
        {
            MS.Debug.Core.Log("Creating unnamed human player");
        }

        public HumanPlayer(string name)
            : base(name)
        {
            MS.Debug.Core.Log("Creating human player " + name);
        }
    }
}

