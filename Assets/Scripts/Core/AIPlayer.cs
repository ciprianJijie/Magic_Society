using System;

namespace MS.Model
{
    [System.Serializable]
    public class AIPlayer : Player
    {
        public AIPlayer()
            : base()
        {
            MS.Debug.Core.Log("Creating unnamed AI player");
        }

        public AIPlayer(string name)
            :base(name)
        {
            MS.Debug.Core.Log("Creating AI player " + name);
        }
    }
}

