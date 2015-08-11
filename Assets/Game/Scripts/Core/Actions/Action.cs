using UnityEngine;
using System.Collections;

namespace MS.Core.Actions
{
    public abstract class Action : MonoBehaviour
    {
        protected bool      m_IsPerforming;

        // Events
        public delegate void ActionEvent();

        public static void DefaultAction() {}

        public event ActionEvent OnStarted      =   DefaultAction;
        public event ActionEvent OnUpdated      =   DefaultAction;
        public event ActionEvent OnFinished     =   DefaultAction;
        // ---

        /// <summary>
        /// Starts the execution of the action.
        /// </summary>
        public void Perform()
        {
            StartAction();
        }

        protected virtual void StartAction()
        {
            m_IsPerforming = true;
            OnStarted();
            UnityEngine.Debug.Log("Action " + this + " started");
        }

        /// <summary>
        /// How the action must be updated every frame as long as it is active.
        /// </summary>
        protected abstract void UpdateAction();

        protected virtual void FinishAction()
        {
            m_IsPerforming = false;
            OnFinished();
            UnityEngine.Debug.Log("Action " + this + " finished");
            Destroy(this.gameObject);
        }

        /// <summary>
        /// Checks if the actions is finished. When this is true, the actions will stop and will be deleted.
        /// </summary>
        /// <returns><c>true</c> if this action is finished; otherwise, <c>false</c>.</returns>
        protected abstract bool IsFinished();

        protected virtual void Update()
        {
            if (m_IsPerforming)
            {
                if (IsFinished())
                {
                    FinishAction();
                }
                else
                {
                    UpdateAction();
                    OnUpdated();
                }
            }
        }
    }
}
