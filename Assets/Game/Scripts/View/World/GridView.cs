using UnityEngine;
using System.Collections.Generic;

namespace MS
{
    public class GridView : View<Grid>
    {
        public float            HexagonSize = 1f;
        public TileView         TileViewPrefab;
        public TileSelector     Selector;

        [HideInInspector]
        public float         TileWidth;

        [HideInInspector]
        public float         TileHeight;

        private List<TileView>  m_TileViews;

        public int HorizontalSize
        {
            get
            {
                if (m_Model != null) return m_Model.HorizontalSize;
                else return 0;
            }
        }

        public int VerticalSize
        {
            get
            {
                if (m_Model != null) return m_Model.VerticalSize;
                else return 0;
            }
        }

        public override void UpdateView()
        {
            TileWidth   =   Hexagon.Width(HexagonSize);
            TileHeight  =   Hexagon.Height(HexagonSize);

            if (m_TileViews != null)
            {
                foreach (TileView tile in m_TileViews)
                {
                    Destroy(tile.gameObject);
                }

                m_TileViews.Clear();
            }
            else
            {
                m_TileViews = new List<TileView>();
            }

            TileView    tileView;

            for (int x = 0; x < m_Model.HorizontalSize; ++x)
            {
                for (int y = 0; y < m_Model.VerticalSize; ++y)
                {
                    GameObject obj = Instantiate(TileViewPrefab.gameObject, LocalToWorld(x, y), Quaternion.identity) as GameObject;

                    tileView = obj.GetComponent<TileView>();
                    tileView.transform.SetParent(this.transform);
                    tileView.Owner = this;

                    tileView.name = "Tile [" + x + "," + y + "]";

                    tileView.BindTo(m_Model.GetTile(x, y));
                    tileView.UpdateView();

                    m_TileViews.Add(tileView);
                }
            }

            Selector.enabled = true;
        }

        public void UpdateView(int x, int y)
        {
            // TODO: This is horrible, refactor this
            foreach (TileView tileView in m_TileViews)
            {
                if (tileView.name.Contains("[" + x + "," + y + "]"))
                {
                    tileView.UpdateView();
                    break;
                }
            }

            Selector.enabled = true;
        }

        /// <summary>
        /// Transforms from axial local coordinates to world coordinates.
        /// </summary>
        public Vector3 LocalToWorld(float x, float y)
        {
            Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);

            x = x - (float) m_Model.HorizontalSize / 2f;
            y = y - (float) m_Model.VerticalSize / 2f;

            pos.x = this.transform.position.x + (HexagonSize * Mathf.Sqrt (3f) * (x + y / 2.0f));
            pos.y = this.transform.position.y;
            pos.z = this.transform.position.z + (HexagonSize * 3.0f / 2.0f * y);

            return pos;
        }

        /// <summary>
        /// Transform a world coordinate to axial local coordinate.
        /// </summary>
        public Vector2 WorldToLocal(float x, float y, float z)
        {
            Vector2 pos;

            float approximateX;
            float approximateY;

            approximateX = (x * (Mathf.Sqrt(3.0f) / 3.0f) - (z / 3.0f) / HexagonSize);
            approximateY = (z * (2.0f / 3.0f) / HexagonSize);

            approximateX += (float)m_Model.HorizontalSize / 2f;
            approximateY += (float)m_Model.VerticalSize / 2f;

            // And now we find the nearest hexagon to that position
            Vector3 cubePos = Hexagon.AxialToCube(approximateX, approximateY);
            cubePos = Hexagon.RoundToCube(cubePos);

            pos = Hexagon.CubeToAxial(cubePos);

            return pos;
        }

        public Vector2 WorldToLocal(Vector3 v)
        {
            return WorldToLocal(v.x, v.y, v.z);
        }

        public Vector3 GetSelectorPosition(int x, int y)
        {
            Vector3     worldPosition;
            float       height;

            worldPosition = LocalToWorld(x, y);

            if (x < 0 || y < 0 || x >= m_Model.HorizontalSize || y >= m_Model.VerticalSize)
            {
                height = 0;
            }
            else
            {
                height = m_Model.GetTile(x, y).Height;
            }

            return worldPosition + Vector3.up * height * 0.5f;
        }

    }
}
