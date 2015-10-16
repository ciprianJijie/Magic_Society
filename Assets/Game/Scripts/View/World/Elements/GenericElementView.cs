using UnityEngine;
using MS.Model;
using System;

namespace MS.Views.World
{
    public class GenericElementView : TerrainDependantView<Model.MapElement>
    {
        public override void UpdateView(MapElement element)
        {
            throw new NotImplementedException();
        }
    }
}
