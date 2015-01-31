using UnityEngine;

namespace MS.View
{
    public class TileView : View<MS.Model.Tile>
	{
        public override void UpdateView()
        {
            GetComponent<SpriteRenderer>().sprite = MS.Manager.ResourceManager.GetSprite(m_model.Type);
        }
	}
}