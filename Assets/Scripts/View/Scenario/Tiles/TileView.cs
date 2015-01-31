using UnityEngine;
using SimpleJSON;

namespace MS.View
{
    public abstract class TileView : View<MS.Model.Tile>
	{
        public Vector2 Location;

        public override void UpdateView()
        {
            SpriteRenderer renderer;

            renderer                =   GetComponent<SpriteRenderer>();
            renderer.sprite         =   MS.Manager.ResourceManager.GetSprite(m_model.Type);
            renderer.sortingOrder   =   - (int)Location.y;
        }
	}
}