using UnityEngine;
using System;

namespace MS
{
    public abstract class View<T> : MonoBehaviour, IModelRelated<T>, IObjectRelated, IUpdatableView, IUpdatableView<T> where T : ModelElement
    {
        protected T m_Model;
        protected Transform m_Transform;

        protected ViewEvent<T> m_OnDestroyed;

        public event ViewEvent OnDestroyed;

        event ViewEvent<T> IUpdatableView<T>.OnDestroyed
        {
            add
            {
                m_OnDestroyed += value;
            }

            remove
            {
                m_OnDestroyed -= value;
            }
        }

        protected void ReportDestruction(View<T> view)
        {
            if (m_OnDestroyed != null)
            {
                m_OnDestroyed(view);
            }
        }


        public Transform Transform
        {
            get
            {
                return m_Transform;
            }
        }

        public GameObject Object
        {
            get
            {
                return this.gameObject;
            }
        }

        public T Model
        {
            get
            {
                return m_Model;
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
        public virtual void UpdateView()
        {

        }

        public abstract void UpdateView(T element);

        public bool IsViewOf(T model)
        {
            return model == m_Model;
        }

        public bool IsViewOf(ModelElement element)
        {
            return element == m_Model;
        }
    }
}
