using UnityEngine;

namespace MS
{
	public class TileSelector : MonoBehaviour
	{
        public MouseToGrid InputToReact;

		public void SelectTile(int x, int y)
		{
            Vector3 position;

            position = InputToReact.GridView.GetSelectorPosition(x, y);

			this.transform.position = position;
        }

		protected void Start()
		{
			// Subscribe to events
			InputToReact.OnMouseOver += SelectTile;
		}

		protected void OnDestroy()
		{
			// Unsubscribe to events
			InputToReact.OnMouseOver -= SelectTile;
		}
    }
}
