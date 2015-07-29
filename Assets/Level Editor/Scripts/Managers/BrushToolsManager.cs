using UnityEngine;

namespace MS
{
    public class BrushToolsManager : MonoBehaviour
    {
        public GridController   GridController;
        public int              Radius;

        protected TerrainBrush  TerrainBrush;
        protected HeightBrush   HeightBrush;
        protected Brush         ActiveBrush;


        public void ChangeTerrain(string terrainType)
        {
            TerrainBrush.TerrainType = EnumUtils.ParseEnum<Tile.Terrain>(terrainType);
        }

        public void ChangeHeight(int height)
        {
            HeightBrush.Height = height;
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
            }
        }

        public void Draw(int x, int y)
        {
            ActiveBrush.Draw(x, y, Radius);
            GridController.UpdateView(x, y, true);
        }

        // Unity Methods

        protected void Start()
        {
            TerrainBrush    =   new TerrainBrush();
            HeightBrush     =   new HeightBrush();

            TerrainBrush.GridController   =   GridController;
            HeightBrush.GridController    =   GridController;

            SetActiveBrush("Terrain");
        }
    }
}
