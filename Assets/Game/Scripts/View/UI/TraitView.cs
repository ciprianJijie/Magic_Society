using MS.Model;
using UnityEngine.UI;

namespace MS.Views.UI
{
    public class TraitView : View<Model.Trait>
    {
        public LocalizedText NameLabel;

        public override void UpdateView(Trait element)
        {
            NameLabel.ID = element.Name;

            NameLabel.UpdateText();

            m_Model = element;          
        }
    }
}
