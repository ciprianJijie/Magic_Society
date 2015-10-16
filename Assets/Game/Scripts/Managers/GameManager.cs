using UnityEngine;

namespace MS.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public Controllers.World.WorldController WorldController;
        public int WorldRings = 2;

        [HideInInspector]
        public Model.Game Game;
        [HideInInspector]
        public Model.City SelectedCity;

        protected void Start()
        {
            Game = new Model.Game();

            Game.New(3, 1);

            var view = WorldController.CreateView(Game.World);

            view.UpdateView(Game.World);
        }
    }
}
