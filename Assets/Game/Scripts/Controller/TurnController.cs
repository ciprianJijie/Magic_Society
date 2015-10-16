using UnityEngine;
using UnityEngine.UI;

namespace MS.Controllers
{
    public class TurnController : Controller<MS.Views.TurnView, MS.Model.Turns>
    {
        [HideInInspector]
        public Button NextButton;

        public override IUpdatableView<MS.Model.Turns> CreateView(MS.Model.Turns modelElement)
        {
            var view = base.CreateView(modelElement);

            NextButton = (view as Views.TurnView).NextButton;
            NextButton.onClick.AddListener(() => NextTurn());

            return view;
        }

        public void NextTurn()
        {
            m_MainView.Model.CurrentTurn.CurrentPhase.Finish();
            m_MainView.UpdateView();
        }

        protected void Start()
        {
            CreateView(Managers.GameManager.Instance.Game.Turns);

            m_MainView.Model.NextTurn();

            m_MainView.UpdateView();
        }
    }
}