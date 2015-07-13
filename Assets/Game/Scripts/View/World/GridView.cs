using UnityEngine;
using System.Collections.Generic;

namespace MS
{
    public class GridView : View<Grid>
    {
        public float            HexagonSize = 1f;
        public TileView         TileViewPrefab;
        
        private Plane           m_Plane;
        private List<TileView>  m_TileViews;

        public override void UpdateView()
        {
            Debug.Core.Log("Updating grid view.");

            m_Plane = new Plane(this.transform.up, this.transform.position);

            if (m_TileViews != null)
            {
                m_TileViews.Clear();
            }
            else
            {
                m_TileViews = new List<TileView>();
            }

            TileView    tileView;
            Vector2     hexagonCenter;
            Vector2     point;
            
            Gizmos.color = Color.magenta;
            
            for (int x = 0; x < m_Model.HorizontalSize; ++x)
            {
                for (int y = 0; y < m_Model.VerticalSize; ++y)
                {
                    hexagonCenter = Hexagon.OffsetToWorld(x, y, HexagonSize);

                    GameObject obj = Instantiate(TileViewPrefab.gameObject, ProjectToPlane(hexagonCenter), Quaternion.identity) as GameObject;

                    tileView = obj.GetComponent<TileView>();
                    tileView.transform.SetParent(this.transform);

                    m_TileViews.Add(tileView);
                }
            }
        }

        protected Vector3 ProjectToPlane(Vector3 point)
        {
            return this.transform.position + point;
        }

        protected Vector3 ProjectToPlane(Vector2 point)
        {
            return ProjectToPlane(new Vector3(point.x, point.y, 0f));
        }


    }
}
