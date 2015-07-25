using UnityEngine;
using System.Collections;

namespace MS
{
    public class MouseToGrid : MonoBehaviour
    {
        public GridController GridController;

        // Events
        public delegate void GridEvent(int x, int y);

        protected static void DefaultAction(int x, int y) {}

        public event GridEvent OnMouseOver          =   DefaultAction;
        public event GridEvent OnMouseLeftClick     =   DefaultAction;
        public event GridEvent OnMouseRightClick    =   DefaultAction;

        private Plane       m_ProjectionPlane;

        protected void Start()
        {
            m_ProjectionPlane = new Plane(GridController.transform.up, GridController.transform.position);
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

                tilePosition = GridController.WorldToLocal(mousePosition);

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

            hSize       =   Mathf.Max(1, GridController.Grid.HorizontalSize);
            vSize       =   Mathf.Max(1, GridController.Grid.VerticalSize);
            tileWidth   =   Mathf.Max(1, GridController.TileWidth);
            tileHeight  =   Mathf.Max(1, GridController.TileHeight);
            halfWidth   =   (hSize / 2f) * tileWidth;
            halfHeight  =   (vSize / 2f) * tileHeight;

            topLeft = new Vector3(  GridController.transform.position.x - halfWidth,
                                    GridController.transform.position.y,
                                    GridController.transform.position.z + halfHeight);

            topRight = new Vector3(  GridController.transform.position.x + halfWidth,
                                    GridController.transform.position.y,
                                    GridController.transform.position.z + halfHeight);

            bottomRight = new Vector3(  GridController.transform.position.x + halfWidth,
                                    GridController.transform.position.y,
                                    GridController.transform.position.z - halfHeight);

            bottomLeft = new Vector3(  GridController.transform.position.x - halfWidth,
                                    GridController.transform.position.y,
                                    GridController.transform.position.z - halfHeight);

            Gizmos.color = Color.magenta;

            Gizmos.DrawLine(GridController.transform.position, GridController.transform.position + GridController.transform.up * 3f);
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
    }
}
