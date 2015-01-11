using UnityEngine;

namespace MS.View
{
    public class TileView : View
	{
		public Sprite Sprite;

        private MS.Model.Tile m_tile;

        public override void Init(MS.Model.ModelElement model)
        {
            m_tile = model as MS.Model.Tile;

            UpdateView();
        }

        public override void UpdateView()
        {
            Sprite = MS.Managers.ResourceManager.GetSprite(m_tile.Type);
        }
	}
}