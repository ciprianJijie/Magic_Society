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
                Show(Model.Game.Instance.Personalities);
            }
        }

        public void Show(IEnumerable<Model.Personality> personalities)
        {
            foreach (Model.Personality personality in personalities)
            {
                //if (personality.Owner == Model.Game.Instance.Turns.CurrentTurn.Player)
                //{
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
                //}                
            }

            MenuHolder.SetActive(true);
            PersonalityController.UpdateAllViews();
            m_Visible = true;

            // Free unused portrait images
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
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
        
        protected void OnTurnStarted()
        {
            if (m_Visible)
            {
                PersonalityController.UpdateAllViews();
                
                // Free unused portrait images
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
            }
        }

        public void SubscribeToEvents()
        {
            GameInputManager.OnPersonalitiesMenu        +=  Toggle;
            Model.Game.Instance.Turns.OnFirstPlayerTurn +=  OnTurnStarted;
        }

        public void UnsubscribeToEvents()
        {
            GameInputManager.OnPersonalitiesMenu        -=  Toggle;
            Model.Game.Instance.Turns.OnFirstPlayerTurn -=  OnTurnStarted;
        }
    }
}
