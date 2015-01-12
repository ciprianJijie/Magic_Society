using UnityEngine;
using System;

namespace MS.View
{
    public abstract class View<T> : MonoBehaviour where T : MS.Model.ModelElement
    {
        protected T m_model;

        /// <summary>
        /// Binds this view with an element of the model, so what the view shows is
        /// the information from the element of the model.
        /// </summary>
        /// <param name="model">Element of the model to show using this view.</param>
        public void BindTo(T model)
        {
            m_model = model;
        }

        /// <summary>
        /// Updates the view to show the state of the model.
        /// </summary>
        public abstract void UpdateView();
    }
}

