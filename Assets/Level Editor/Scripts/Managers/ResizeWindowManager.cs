using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MS
{
    public class ResizeWindowManager : MonoBehaviour
    {
        public Slider   HorizontalSlider;
        public Slider   VerticalSlider;
        public Text     SizeText;
        
        public void OnCancel()
        {
            Destroy(this.gameObject);
        }
        
        public void OnResize()
        {
            // TODO: Send evet to the LevelEditorManager
            LevelEditorManager.Instance.Resize((int)HorizontalSlider.value, (int)VerticalSlider.value);

            Destroy(this.gameObject);
        }

        public void OnSizeChanged()
        {
            string sizeString;

            sizeString = string.Format("{0}x{1}", HorizontalSlider.value, VerticalSlider.value);

            SizeText.text = sizeString;
        }
    }
}
