using UnityEngine;using UnityEngine.UI;namespace MS.UI{    /// <summary>
    /// Represents an individual element of a ComboBox.
    /// </summary>    public class ComboBoxItem : MonoBehaviour    {
        // =========================== Variables =========================== //

        /// <summary>
        /// Button used to trigger the selection event.
        /// </summary>
        public Button   Button;

        /// <summary>
        /// Text to display what option does this item represent.
        /// </summary>
        public Text     Label;

        // ============================= Events ============================ //

        /// <summary>
        /// Event of an individual item.
        /// </summary>
        /// <param name="text">Text of the option carried by this item.</param>
        public delegate void ComboBoxItemEvent(string text);        /// <summary>
        /// 
        /// </summary>        public event ComboBoxItemEvent OnSelected;

        // ============================ Methods ============================ //

        /// <summary>
        /// Triggers the event of selecting an item from the list.
        /// </summary>
        /// <param name="text"></param>
        protected void ReportItemSelected(string text)        {            if (OnSelected != null)            {                OnSelected(text);            }        }

        // ============================= Unity ============================= //

        protected void Start()        {            Button.onClick.AddListener(() => ReportItemSelected(Label.text));        }    }}