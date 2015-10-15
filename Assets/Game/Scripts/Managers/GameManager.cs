
namespace MS.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public Controllers.World.WorldController WorldController;

        protected Model.World.World World;

        protected void Start()
        {
            World = new Model.World.World(2);

            World.GenerateRandom();

            var view = WorldController.CreateView(World);

            view.UpdateView(World);
        }
    }
}
