
using System;

namespace MS.Controllers.UI
{
    public class TraitController : Controller<Views.UI.TraitView, Model.Trait>, IEventListener
    {
        public void SubscribeToEvents()
        {
            Model.Game.Instance.Turns.OnAllTurnsFinished += OnTurnStarted;
        }

        public void UnsubscribeToEvents()
        {
            Model.Game.Instance.Turns.OnAllTurnsFinished -= OnTurnStarted;
        }

        protected void OnTurnStarted()
        {
            ClearViews();
        }

        protected void Start()
        {
            SubscribeToEvents();
        }

        protected void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
