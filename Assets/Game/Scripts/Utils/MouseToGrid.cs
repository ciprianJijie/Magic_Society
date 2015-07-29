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

            if (GridController.Grid != null &&
                m_ProjectionPlane.Raycast(ray, out distance) &&
                UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
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
    }
}
