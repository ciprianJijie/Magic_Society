
namespace MS.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public Controllers.World.WorldController WorldController;
        public int WorldRings = 2;

        protected Model.World.World World;

        protected void Start()
        {
            World = new Model.World.World(WorldRings);

            World.GenerateRandom();

            var view = WorldController.CreateView(World);

            view.UpdateView(World);
        }
    }
}
