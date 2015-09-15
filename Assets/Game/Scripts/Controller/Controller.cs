using UnityEngine;
using System.Collections.Generic;
using System;

namespace MS
{
    public abstract class Controller<T, R> : MonoBehaviour, IController, IViewCreator<R> where T : class, IUpdatableView<R>, IObjectRelated where R : ModelElement
    {
        public T            ViewPrefab;
        public Transform    Holder;

        protected IList<T>  m_Views;
        protected T         m_MainView;

        protected virtual void Initialize()
        {
            m_Views = new List<T>();
        }

        public virtual IUpdatableView<R> CreateView(R modelElement, T viewPrefab)
        {
            T view;

            var obj = Utils.Instantiate(viewPrefab.Object, Holder, Holder.position, Holder.rotation);

            view = obj.GetComponent<T>();

            view.BindTo(modelElement);
            view.OnDestroyed += OnViewDestroyed;

            m_Views.Add(view);

            m_MainView = view;

            return view;
        }

        public IUpdatableView<R> CreateView(ModelElement element)
        {
            return CreateView(element as R);
        }

        public virtual IUpdatableView<R> CreateView(R modelElement)
        {
            return CreateView(modelElement, ViewPrefab);
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
            m_Views.Clear();
        }

        public virtual void UpdateAllViews()
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

        protected void OnViewDestroyed(IUpdatableView<R> view)
        {
            view.OnDestroyed -= OnViewDestroyed;
            m_Views.Remove(view as T);
        }

        void Awake()
        {
            Initialize();
        }

        public void UpdateView(ModelElement element)
        {
            throw new NotImplementedException();
        }

        IUpdatableView IViewCreator.CreateView(ModelElement modelElement)
        {
            return CreateView(modelElement as R) as IUpdatableView;
        }

        public bool HasViewFor(ModelElement modelElement)
        {
            return HasViewFor(modelElement as R);
        }

        public IUpdatableView FindView(ModelElement modelElement)
        {
            return FindView(modelElement as R) as IUpdatableView;
        }

        public void DestroyView(ModelElement modelElement)
        {
            DestroyView(modelElement as R);
        }
    }
}
