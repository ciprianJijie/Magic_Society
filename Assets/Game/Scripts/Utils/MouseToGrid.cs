using UnityEngine;
using System.Collections;

namespace MS
{
    public class MouseToGrid : MonoBehaviour
    {
        public GridView GridView;

        // Events
        public delegate void GridEvent(int x, int y);

        protected static void DefaultAction(int x, int y) {}

        public event GridEvent OnMouseOver          =   DefaultAction;
        public event GridEvent OnMouseLeftClick     =   DefaultAction;
        public event GridEvent OnMouseRightClick    =   DefaultAction;

        private Plane       m_ProjectionPlane;

        protected void Start()
        {
            m_ProjectionPlane = new Plane(GridView.transform.up, GridView.transform.position);
        }

        protected void Update()
        {
            Vector3     mousePosition;
            Vector2     tilePosition;
            Ray         ray;
            float       distance;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (m_ProjectionPlane.Raycast(ray, out distance))
            {
                mousePosition = ray.GetPoint(distance);

                tilePosition = GridView.WorldToLocal(mousePosition);

                if (Input.GetMouseButtonDown(0))
                {
                    OnMouseLeftClick((int)tilePosition.x, (int)tilePosition.y);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    OnMouseRightClick((int)tilePosition.x, (int)tilePosition.y);
                }
                else
                {
                    OnMouseOver((int)tilePosition.x, (int)tilePosition.y);
                }
            }
        }

        protected void OnDrawGizmosSelected()
        {
            int     hSize;
            int     vSize;
            float   tileWidth;
            float   tileHeight;
            float   halfWidth;
            float   halfHeight;
            Vector3 topLeft;
            Vector3 topRight;
            Vector3 bottomRight;
            Vector3 bottomLeft;

            hSize       =   Mathf.Max(1, GridView.HorizontalSize);
            vSize       =   Mathf.Max(1, GridView.VerticalSize);
            tileWidth   =   Mathf.Max(1, GridView.TileWidth);
            tileHeight  =   Mathf.Max(1, GridView.TileHeight);
            halfWidth   =   (hSize / 2f) * tileWidth;
            halfHeight  =   (vSize / 2f) * tileHeight;

            topLeft = new Vector3(  GridView.transform.position.x - halfWidth,
                                    GridView.transform.position.y,
                                    GridView.transform.position.z + halfHeight);

            topRight = new Vector3(  GridView.transform.position.x + halfWidth,
                                    GridView.transform.position.y,
                                    GridView.transform.position.z + halfHeight);

            bottomRight = new Vector3(  GridView.transform.position.x + halfWidth,
                                    GridView.transform.position.y,
                                    GridView.transform.position.z - halfHeight);

            bottomLeft = new Vector3(  GridView.transform.position.x - halfWidth,
                                    GridView.transform.position.y,
                                    GridView.transform.position.z - halfHeight);

            Gizmos.color = Color.magenta;

            Gizmos.DrawLine(GridView.transform.position, GridView.transform.position + GridView.transform.up * 3f);
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
    }
}
