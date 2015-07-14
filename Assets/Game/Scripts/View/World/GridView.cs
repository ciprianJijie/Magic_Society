using UnityEngine;
using System.Collections.Generic;

namespace MS
{
    public class GridView : View<Grid>
    {
        public float            HexagonSize = 1f;
        public TileView         TileViewPrefab;

        protected float         TileWidth;
        protected float         TileHeight;

        private Plane           m_Plane;
        private List<TileView>  m_TileViews;

        public override void UpdateView()
        {
            m_Plane     =   new Plane(this.transform.up, this.transform.position);
            TileWidth   =   Hexagon.Width(HexagonSize);
            TileHeight  =   Hexagon.Height(HexagonSize);

            if (m_TileViews != null)
            {
                m_TileViews.Clear();
            }
            else
            {
                m_TileViews = new List<TileView>();
            }

            TileView    tileView;
            
            Gizmos.color = Color.magenta;
            
            for (int x = 0; x < m_Model.HorizontalSize; ++x)
            {
                for (int y = 0; y < m_Model.VerticalSize; ++y)
                {
                    GameObject obj = Instantiate(TileViewPrefab.gameObject, LocalToWorld(x, y), Quaternion.identity) as GameObject;

                    tileView = obj.GetComponent<TileView>();
                    tileView.transform.SetParent(this.transform);

                    tileView.name = "Tile [" + x + "," + y + "]";

                    tileView.BindTo(m_Model.GetTile(x, y));
                    tileView.UpdateView();

                    m_TileViews.Add(tileView);
                }
            }
        }

        protected Vector3 LocalToWorld(float x, float y)
        {
            Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
            
            x = x - (float) m_Model.HorizontalSize / 2f;
            y = y - (float) m_Model.VerticalSize / 2f;

            pos.x = this.transform.position.x + (HexagonSize * Mathf.Sqrt (3f) * (x + y / 2.0f));
            pos.z = this.transform.position.z + (HexagonSize * 3.0f / 2.0f * y);
            
            return pos;
        }

        protected Vector2 WorldToLocal(float x, float y, float z)
        {
            Vector2 pos;
            
            float approximateX;
            float approximateY;
            
            approximateX = (x * Mathf.Sqrt(3) / 3.0f - y / 3.0f) / TileWidth - this.transform.position.x;
            approximateY = (y * 2.0f / 3.0f ) / TileHeight - this.transform.position.y;
            
            approximateX += m_Model.HorizontalSize / 2;
            approximateY += m_Model.VerticalSize / 2;
            
            // And now we find the nearest hexagon to that position
            Vector3 cubePos = Hexagon.AxialToCube (approximateX, approximateY);
            cubePos = Hexagon.RoundToCube (cubePos);
            
            pos = Hexagon.CubeToAxial(cubePos);
            
            return pos;
        }


    }
}
