using System;
using System.Collections.Generic;
using MS.Model;
using MS.Model.Kingdom;
using UnityEngine;

namespace MS.Controllers.Kingdom
{
    public class CityController : Controller<CityView, Model.City>
    {
        public TerrainDependant TerrainDependantPrefabs;

        public override IUpdatableView<City> CreateView(City modelElement)
        {
            GameObject  prefab;
            Tile        tile;
            CityView    view;

            tile    =   GameController.Instance.Game.Map.Grid.GetTile(modelElement.X, modelElement.Y);
            prefab  =   TerrainDependantPrefabs.SelectPrefab(tile.TerrainType);
            view    =   prefab.GetComponent<CityView>();

            return base.CreateView(modelElement, view);
        }

        public override void UpdateAllViews()
        {
            foreach (CityView view in m_Views)
            {
                foreach (IUpdatableView childView in view.GetComponents<IUpdatableView>())
                {
                    childView.UpdateView();
                }
            }
        }
    }
}