﻿using UnityEngine;
using System.Collections;

namespace MS
{
    public class UpdateViewOnStart : MonoBehaviour
    {
        public GridController           GridController;
        public MapElementsManager    ElementsController;

        protected void Start()
        {
            GridController.Show(GameController.Instance.Game.Map.Grid);
            ElementsController.Show(GameController.Instance.Game.Map.Grid);
        }
    }
}
