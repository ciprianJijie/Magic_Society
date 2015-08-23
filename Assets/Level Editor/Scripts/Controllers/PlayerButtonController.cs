using UnityEngine;
using UnityEngine.UI;
using MS.Controllers;

namespace MS.Controllers.Editor
{
    public class PlayerButtonController : Controller<Views.Editor.PlayerView, Model.Player>
    {
        public Button RemoveButton;

        public Events.StringEvent OnRemove = Events.DefaultAction;

        private void OnRemoveButtonPressed(string playerName)
        {
            OnRemove(playerName);
        }

        public override IUpdatableView<MS.Model.Player> CreateView(MS.Model.Player modelElement)
        {
            var view = base.CreateView(modelElement);

            RemoveButton = (view as MS.Views.Editor.PlayerView).RemoveButton;

            return view;
        }

        protected void Start()
        {
            RemoveButton.onClick.AddListener(() => OnRemoveButtonPressed(m_Views[0].Model.Name));
        }
    }
}
