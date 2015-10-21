using UnityEngine;
using UnityEngine.UI;

namespace MS.Views
{
    public class TurnView : MS.View<Model.Turns>
    {
        public Text     PlayerLabel;
        public Text     PhaseLabel;
        public Text     TurnCountLabel;
        public Button   NextButton;

        public override void UpdateView(MS.Model.Turns element)
        {
            PlayerLabel.text        =   element.CurrentTurn.Player.Name;
            PhaseLabel.text         =   element.CurrentTurn.CurrentPhase.Name;
            TurnCountLabel.text     =   "Turn " + element.TurnCounter;

            m_Model = element;
        }        
    }
}