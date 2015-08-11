
namespace MS
{
	public class GameController : Singleton<GameController>
	{
        protected Game m_Game;

        public Game Game
        {
            get
            {
                return m_Game;
            }
        }

        protected void Start()
        {
            m_Game = new Game();
        }
	}
}
