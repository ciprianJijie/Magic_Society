using UnityEngine;
using System.Collections.Generic;
using System;

namespace MS
{
    public abstract class Controller<T, R> : MonoBehaviour, IViewCreator<R> where T : class, IUpdatableView<R>, IObjectRelated where R : ModelElement
    {
        public T            ViewPrefab;
        public Transform    Holder;

        protected IList<T>  m_Views;

        protected virtual void Initialize()
        {
            m_Views = new List<T>();
        }

        public IUpdatableView<R> CreateView(R modelElement)
        {
            T view;

            var obj = Utils.Instantiate(ViewPrefab.Object, Holder, Holder.position, Holder.rotation);

            view = obj.GetComponent<T>();

            view.BindTo(modelElement);
            view.OnDestroyed += OnViewDestroyed;

            m_Views.Add(view);

            return view;
        }

        public T FindView(R model)
        {
            foreach (T view in m_Views)
            {
                if (view.IsViewOf(model))
                {
                    return view;
                }
            }
            return null;
        }

        IUpdatableView<R> IViewCreator<R>.FindView(R modelElement)
        {
            return FindView(modelElement);
        }

        public void DestroyView(R modelElement)
        {
            IObjectRelated viewObj;

            viewObj = FindView(modelElement);

            if (viewObj != null)
            {
                Destroy(viewObj.Object);
            }
        }

        public void ClearViews()
        {
            foreach (T view in m_Views)
            {
                Destroy(view.Object);
            }
        }

        public void UpdateAllViews()
        {
            foreach (IUpdatableView view in m_Views)
            {
                view.UpdateView();
            }
        }

        public bool HasViewFor(R modelElement)
        {
            return FindView(modelElement) != null;
        }

        private void OnViewDestroyed(IUpdatableView<R> view)
        {
            view.OnDestroyed -= OnViewDestroyed;
            m_Views.Remove(view as T);
        }

        void Awake()
        {
            Initialize();
        }
    }
}
