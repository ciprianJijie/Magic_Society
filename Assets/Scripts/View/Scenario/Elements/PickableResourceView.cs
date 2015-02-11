using UnityEngine;
using MS.Manager;

namespace MS.View
{
    public class PickableResourceView : MapElementView
    {
        public Sprite WoodSprite;

        public override void UpdateView()
        {
            MS.Model.PickableResource res;

            res = m_model as MS.Model.PickableResource;

            if (res.Storage.Resource == GameManager.Game.Scenario.Map.GetResource("Wood"))
            {
                GetComponent<SpriteRenderer>().sprite = WoodSprite;
            }
        }

        public override string ToString()
        {
            MS.Model.PickableResource res;

            res = m_model as MS.Model.PickableResource;

            return res.ToString();
        }
    }
}
