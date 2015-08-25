using UnityEngine;

namespace MS.Core.Actions
{
    /// <summary>
    /// Allows for a lerped movement of an scene object.
    /// </summary>
    public class Movement : Action
    {
        protected Transform         m_Target;

        protected Vector3           m_InitialPosition;
        protected Vector3           m_FinalPosition;

        // Speed based
        protected float             m_Speed;

        // Time based
        protected float             m_Duration;
        protected AnimationCurve    m_SpeedCurve;

        protected bool              m_StartOnAwake;

        protected bool              m_IsMoving;
        protected bool              m_TimeBased;

        protected float             m_TimeStarted;

        public void Perform(Transform target, Vector3 ini, Vector3 end, float speed)
        {
            m_Target            =   target;
            m_InitialPosition   =   ini;
            m_FinalPosition     =   end;
            m_Speed             =   speed;
            m_IsMoving          =   true;
            m_TimeBased         =   false;
            m_Target.position   =   m_InitialPosition;

            base.Perform();
        }

        public void Perform(Transform target, Vector3 ini, Vector3 end, float duration, AnimationCurve curve)
        {
            m_Target            =   target;
            m_InitialPosition   =   ini;
            m_FinalPosition     =   end;
            m_Duration          =   duration;
            m_SpeedCurve        =   curve;
            m_IsMoving          =   true;
            m_TimeBased         =   true;
            m_TimeStarted       =   Time.time;

            base.Perform();
        }

        protected override void UpdateAction()
        {
            if (m_TimeBased)
            {
                float alpha;

                alpha = (Time.time - m_TimeStarted) / m_Duration;

                m_Target.position = Vector3.Lerp(m_InitialPosition, m_FinalPosition, m_SpeedCurve.Evaluate(alpha));
            }
            else
            {
                m_Target.position = Vector3.MoveTowards(m_Target.position, m_FinalPosition, m_Speed * Time.deltaTime);
            }
        }

        protected override bool IsFinished()
        {
            if (m_TimeBased)
            {
                return (Time.time > m_TimeStarted + m_Duration);
            }
            else
            {
                return m_Target.position == m_FinalPosition;
            }
        }

        public override string ToString()
        {
            return string.Format("Movement Action");
        }
    }
}
