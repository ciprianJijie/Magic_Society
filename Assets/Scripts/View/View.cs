using UnityEngine;
using System;

namespace MS.View
{
    public abstract class View : MonoBehaviour
    {
        /// <summary>
        /// Inits this view, binding it to an element of the model.
        /// Any changes made to the view should automatically update the model.
        /// </summary>
        /// <param name="model">Model's elemento to bind this view to.</param>
        public abstract void Init(MS.Model.ModelElement model);

        /// <summary>
        /// Updates the view to reflect the state of the model.
        /// </summary>
        public abstract void UpdateView();
    }
}

