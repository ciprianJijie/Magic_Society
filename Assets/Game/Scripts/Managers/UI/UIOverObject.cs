using UnityEngine;

namespace MS
{
    public class UIOverObject : MonoBehaviour
    {
        public Transform    Target;
        public Vector2      Offset;

        private RectTransform m_Rect;

        protected void Start()
        {
            m_Rect = this.gameObject.GetComponent<RectTransform>();

            m_Rect.SetAsFirstSibling();
        }

        void Update()
        {
            Vector2 screenPosition;

            screenPosition = Camera.main.WorldToScreenPoint(Target.position);

            m_Rect.anchoredPosition = new Vector2(  screenPosition.x + Offset.x - Screen.width / 2f,
                                                    screenPosition.y + Offset.y - Screen.height / 2f);
        }
    }

}