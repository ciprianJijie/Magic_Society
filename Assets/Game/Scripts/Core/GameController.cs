
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
	}
}
