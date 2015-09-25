using UnityEngine;using UnityEngine.UI;namespace MS.Managers.UI{    public class ProgressBar : MonoBehaviour    {        public Image    BackgroundImage;        public Image    BarImage;        public int      PaddingLeft;        public int      PaddingRight;        public int      PaddingTop;        public int      PaddingBottom;        public float    MinValue;        public float    MaxValue;        protected float m_CurrentValue;        public void SetValue(float value)
        {
            float alpha;

            m_CurrentValue  =   value;
            alpha           =   (m_CurrentValue - MinValue) / (MaxValue - MinValue);
            
            SetPercentage(alpha);
        }        public void SetPercentage(float percentage)
        {
            Vector2 position;
            float width;
            float height;
            
            width       =   Mathf.Abs((BackgroundImage.rectTransform.sizeDelta.x - PaddingLeft - PaddingRight)) * percentage;
            height      =   BackgroundImage.rectTransform.sizeDelta.y - PaddingTop - PaddingBottom;
            position.x  =   PaddingLeft;
            position.y  =   PaddingBottom;

            BarImage.rectTransform.anchoredPosition = position;
            BarImage.rectTransform.sizeDelta = new Vector2(width, height);
        }    }}