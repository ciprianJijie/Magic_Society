using UnityEngine;
using System.Collections.Generic;
using System;

namespace MS.Controllers.UI
{
    public class PersonalitiesController : MonoBehaviour, IEventListener
    {
        public PersonalityController    PersonalityController;
        public GameObject               MenuHolder;
        public GameInputManager         GameInputManager;

        protected bool m_Visible;

        public void Toggle()
        {
            if (m_Visible)
            {
                Hide();
            }
            else
            {
                Show(Game.Instance.Personalities);
            }
        }

        public void Show(IEnumerable<Model.Personality> personalities)
        {
            foreach (Model.Personality personality in personalities)
            {
                Views.UI.PersonalityView view;

                view = PersonalityController.FindView(personality);

                if (view != null)
                {
                    view.UpdateView(personality);
                }
                else
                {
                    PersonalityController.CreateView(personality);
                }
            }

            PersonalityController.UpdateAllViews();
            MenuHolder.SetActive(true);
            m_Visible = true;
        }

        public void Hide()
        {
            MenuHolder.SetActive(false);
            PersonalityController.ClearViews();
            m_Visible = false;
        }

        protected void Start()
        {
            Hide();
            SubscribeToEvents();
        }

        protected void OnDestroy()
        {
            UnsubscribeToEvents();
        }

        public void SubscribeToEvents()
        {
            GameInputManager.OnPersonalitiesMenu += Toggle;
        }

        public void UnsubscribeToEvents()
        {
            GameInputManager.OnPersonalitiesMenu -= Toggle;
        }
    }
}
