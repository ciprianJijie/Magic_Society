using UnityEngine;using UnityEngine.UI;namespace MS.Managers.UI{    public class ProgressBar : MonoBehaviour    {        public Image    BackgroundImage;        public Image    BarImage;        public Text     CurrentValueLabel;        public Text     MaxValueLabel;        public int      PaddingLeft;        public int      PaddingRight;        public int      PaddingTop;        public int      PaddingBottom;        [HideInInspector]        public float    MinValue;        [HideInInspector]        public float    MaxValue;        protected float m_CurrentValue;        public void UpdateProgressBar(float minValue, float maxValue, float currentValue)
        {
            MinValue                =   minValue;
            MaxValue                =   maxValue;
            MaxValueLabel.text      =   maxValue.ToString();
            CurrentValueLabel.text  =   currentValue.ToString();

            SetValue(currentValue);
        }        public void SetValue(float value)
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

            //width       =   Mathf.Abs((BackgroundImage.rectTransform.sizeDelta.x - PaddingLeft - PaddingRight)) * percentage;
            //height      =   BackgroundImage.rectTransform.sizeDelta.y - PaddingTop - PaddingBottom;
            width       =   Mathf.Abs(BackgroundImage.rectTransform.rect.width - PaddingLeft - PaddingRight) * percentage;
            height      =   BarImage.rectTransform.sizeDelta.y;
            position.x  =   width / 2f + PaddingLeft;
            position.y  =   BarImage.rectTransform.anchoredPosition.y;

            UnityEngine.Debug.Log("Background Width: " + BackgroundImage.rectTransform.rect.width);

            BarImage.rectTransform.anchoredPosition = position;
            BarImage.rectTransform.sizeDelta = new Vector2(width, height);
        }    }}