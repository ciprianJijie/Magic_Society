using UnityEngine;
using UnityEngine.UI;

namespace MS
{
    public class BrushToolsManager : MonoBehaviour
    {
        public GridController           GridController;
        public MapElementsController    MapElementsController;
        public InputField               RadiusInput;
        protected int                   Radius;

        protected TerrainBrush          TerrainBrush;
        protected HeightBrush           HeightBrush;
        protected ObjectBrush           ObjectBrush;
        protected Brush                 ActiveBrush;

        public void UpdateRadius()
        {
            Radius = System.Int32.Parse(RadiusInput.text) - 1;
        }

        public void ChangeTerrain(string terrainType)
        {
            TerrainBrush.TerrainType = EnumUtils.ParseEnum<Tile.Terrain>(terrainType);
        }

        public void ChangeHeight(int height)
        {
            HeightBrush.Height = height;
        }

        public void ChangeObject(string name)
        {
            ObjectBrush.ElementName = name;
        }

        public void SetActiveBrush(string brush)
        {
            switch(brush)
            {
                case "Terrain":
                    ActiveBrush = TerrainBrush;
                    break;

                case "Height":
                    ActiveBrush = HeightBrush;
                    break;
                case "Element":
                    ActiveBrush = ObjectBrush;
                    break;
            }
        }

        public void Draw(int x, int y)
        {
            foreach (Vector2 drawnTile in ActiveBrush.Draw(x, y, Radius, GridController.Grid))
            {
                GridController.UpdateView(drawnTile, true);
                MapElementsController.UpdateView(drawnTile);
            }
        }

        // Unity Methods

        protected void Start()
        {
            TerrainBrush    =   new TerrainBrush();
            HeightBrush     =   new HeightBrush();
            ObjectBrush     =   new ObjectBrush();

            SetActiveBrush("Terrain");
        }
    }
}
