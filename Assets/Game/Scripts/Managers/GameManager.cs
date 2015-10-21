using UnityEngine;

namespace MS.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public Controllers.World.WorldController WorldController;
        public int WorldRings = 2;

        [HideInInspector]
        public Model.City SelectedCity;

        protected void Start()
        {
            Model.Game.Instance.New(3, 1);

            var view = WorldController.CreateView(Model.Game.Instance.World);

            view.UpdateView(Model.Game.Instance.World);
        }
    }
}
