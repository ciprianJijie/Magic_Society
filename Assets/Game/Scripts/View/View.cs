using UnityEngine;
using System;

namespace MS
{
    public abstract class View<T> : MonoBehaviour where T : ModelElement
    {
        protected T m_Model;
        protected Transform m_Transform;

        public T Model { get { return m_Model; } }

        // Events
        protected static void DefaultAction(View<T> view) {}
        public Action<View<T>> OnDestroyed = DefaultAction;

        protected void ReportDestruction(View<T> view) { OnDestroyed(view); }


        public Transform Transform
        {
            get
            {
                return m_Transform;
            }
        }

        public virtual void Awake()
        {
            m_Transform = this.gameObject.GetComponent<Transform>();
        }

        void OnDestroy()
        {
            ReportDestruction(this);
        }

        /// <summary>
        /// Binds this view with an element of the model, so what the view shows is
        /// the information from the element of the model.
        /// </summary>
        /// <param name="model">Element of the model to show using this view.</param>
        public void BindTo(T model)
        {
            m_Model = model;
        }

        /// <summary>
        /// Updates the view to show the state of the model.
        /// </summary>
        public abstract void UpdateView();

        public bool IsViewOf(T model)
        {
            return model == m_Model;
        }
    }
}
