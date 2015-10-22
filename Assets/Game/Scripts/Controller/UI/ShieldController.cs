
using System;

namespace MS.Controllers.UI.Heraldry
{
    public class ShieldController : Controller<Views.UI.Heraldry.ShieldView, Model.Heraldry.Shield>, IEventListener
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
