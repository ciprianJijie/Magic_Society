using UnityEngine;

namespace MS
{
    public class MapElementsController : Controller<MapElementView, MapElement>
    {
        public GridController   GridController;
        protected Grid          m_Grid;

        public void Show(Grid grid)
        {
            m_Grid = grid;

            UpdateView();
        }

        public void UpdateView()
        {
            ClearViews();

            MapElement      element;
            
            for ( int x = 0; x < m_Grid.HorizontalSize; x++)
            {
                for ( int y = 0; y < m_Grid.VerticalSize; y++)
                {
                    element = m_Grid.GetElement(x, y);
                    
                    if (element != null)
                    {
                        var view = CreateView(element);

                        view.name = element.Name;
                        view.transform.position = GridController.LocalToWorld(x, y);
                    }
                }
            }

            foreach (MapElementView view in m_Views)
            {
                view.UpdateView(m_Grid.GetTile(view.Model.X, view.Model.Y));
            }
        }

        public void UpdateView(Vector2 axial)
        {
            UpdateView((int)axial.x, (int)axial.y);
        }

        public void UpdateView(int x, int y)
        {
            ClearView(x, y);

            MapElement      element;
            MapElementView  elementView;

            element = m_Grid.GetElement(x, y);

            if (element != null)
            {
                elementView = FindView(element);

                if (elementView == null)
                {
                    UnityEngine.Debug.Log("View not found. Creating a new one.");
                    elementView = CreateView(element);

                    elementView.name = element.Name;
                    elementView.transform.position = GridController.LocalToWorld(x, y);
                }

                elementView.UpdateView(m_Grid.GetTile(element.X, element.Y));
            }
        }

        public void ClearView(int x, int y)
        {
            MapElementView viewToDelete;

            viewToDelete = null;

            foreach (MapElementView view in m_Views)
            {
                if (view.Model.X == x && view.Model.Y == y)
                {
                    viewToDelete = view;
                    break;
                }
            }

            if (viewToDelete != null)
            {
                m_Views.Remove(viewToDelete);
                Destroy(viewToDelete.gameObject);
            }
        }
    }
}
