using UnityEngine;
using MS.Controllers.UI.Heraldry;

namespace MS.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public Controllers.World.WorldController    WorldController;
        public ShieldController                     ShieldController;
        public Transform                            PlayerShieldController;
        public int WorldRings = 2;

        [HideInInspector]
        public Model.City SelectedCity;

        protected Model.NobleHouse              m_PlayerHouse;
        protected Views.UI.Heraldry.ShieldView  m_PlayerShieldView;

        protected void Start()
        {
            Model.Game.Instance.New(3, 1);

            var view = WorldController.CreateView(Model.Game.Instance.World);

            view.UpdateView(Model.Game.Instance.World);

            ShieldController.Holder =   PlayerShieldController;
            m_PlayerHouse           =   Model.Game.Instance.Players.Find("PLAYER_HUMAN").MainHouse;
            m_PlayerShieldView      =   ShieldController.CreateView(m_PlayerHouse) as Views.UI.Heraldry.ShieldView;

            m_PlayerShieldView.UpdateView(m_PlayerHouse.Shield);
        }
    }
}
