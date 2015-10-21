using System;
using UnityEngine;
using UnityEngine.UI;

namespace MS.Controllers
{
    public class TurnController : Controller<MS.Views.TurnView, MS.Model.Turns>, IEventListener
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
            Model.Game.Instance.Turns.CurrentTurn.CurrentPhase.End();
            m_MainView.UpdateView();
        }

        public void SubscribeToEvents()
        {
            Model.Phase.OnPhaseStarted += OnPhaseStarted;
        }

        public void UnsubscribeToEvents()
        {
            Model.Phase.OnPhaseStarted -= OnPhaseStarted;
        }

        protected void Start()
        {
            SubscribeToEvents();

            CreateView(Model.Game.Instance.Turns);

            Model.Game.Instance.Turns.Start();
            Model.Game.Instance.Turns.Execute();

            m_MainView.UpdateView();
        }

        protected void OnPhaseStarted(Model.Phase phase)
        {
            m_MainView.UpdateView();
        }

        protected void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}