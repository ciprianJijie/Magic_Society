using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MS
{
    public abstract class Controller<T, R> : MonoBehaviour where T : View<R> where R : ModelElement
    {
        public T            ViewPrefab;
        public Transform    Holder;

        protected IList<View<R>>  m_Views;

        protected virtual void Initialize()
        {
            m_Views = new List<View<R>>();
        }

        public virtual T CreateView(R model)
        {
            T view;

            view                    =   Instantiate(ViewPrefab);
            view.Transform.parent   =   Holder;
            view.Transform.position =   Holder.position;
            view.Transform.rotation =   Holder.rotation;

            view.BindTo(model);
            view.UpdateView();
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

        public void ClearViews()
        {
            foreach (T view in m_Views)
            {
                Destroy(view.gameObject);
            }
        }

        private void OnViewDestroyed(View<R> view)
        {
            view.OnDestroyed -= OnViewDestroyed;
            m_Views.Remove(view);
        }

        void Awake()
        {
            Initialize();
        }
    }
}
