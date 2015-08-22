using UnityEngine;
using UnityEngine.UI;

namespace MS.Views.Editor
{
    public class PlayerView : View<MS.Model.Player>
    {
        public InputField   NameLabel;
        public Text         TypeLabel;
        public Button       RemoveButton;

        public override void UpdateView(MS.Model.Player element)
        {
            NameLabel.text = element.Name;

            if (element is MS.Model.AIPlayer)
            {
                TypeLabel.text = "AI";
            }
            else if (element is MS.Model.HumanPlayer)
            {
                TypeLabel.text = "Human";
            }
            else if (element is MS.Model.NeutralPlayer)
            {
                TypeLabel.text = "Neutral";
            }
            else
            {
                TypeLabel.text = "TYPE_NOT_FOUND";
            }
        }

        private void OnNameChanged(string newName)
        {
            Model.Name = newName;
        }

        protected void Start()
        {
            NameLabel.onEndEdit.AddListener((string text) => OnNameChanged(text));
        }
    }
}
