using SimpleJSON;

namespace MS
{
    public class GridPosition : IParseable
    {
        public GridPosition(Grid grid, int x, int y)
        {

        }

        public void FromJSON(JSONNode json)
        {
            // TODO: Search for the correct Grid

            x = json["x"].AsInt;
            y = json["y"].AsInt;
        }

        public JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            json["grid"]    =   Grid.ID;
            json["x"]       =   x.ToString();
            json["y"]       =   y.ToString();

            return json;
        }

        public static GridPosition operator +(GridPosition a, GridPosition b)
        {
            if (a.Grid != b.Grid)
            {
                throw new System.InvalidOperationException("Positions from different grids can't be operated");
            }

            return new GridPosition(a.Grid, a.x + b.x, a.y + b.y);
        }

        public static GridPosition operator -(GridPosition a, GridPosition b)
        {
            if (a.Grid != b.Grid)
            {
                throw new System.InvalidOperationException("Positions from different grids can't be operated");
            }

            return new GridPosition(a.Grid, a.x - b.x, a.y - b.y);
        }

        public Grid     Grid;
        public int      x;
        public int      y;
    }
}
