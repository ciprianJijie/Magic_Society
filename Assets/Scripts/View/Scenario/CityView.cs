using UnityEngine;

namespace MS.View
{
    public class CityView : View<MS.Model.City>
    {
        public override void UpdateView()
        {
            SpriteRenderer renderer;

            renderer = this.gameObject.GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                renderer.sprite = MS.Manager.ResourceManager.GetSprite(m_model as MS.Model.MapElement);
            }
        }
    }
}

