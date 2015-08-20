using UnityEngine;using UnityEngine.UI;using System.Collections.Generic;using System.Collections;namespace MS.UI{    /// <summary>
    /// Controls the behaviour for a ComboBox/DropDown UI control for Unity 4.6 UI system.
    /// 
    /// You can iterate over this class as if it were an IEnumerable<ComboBoxItem> if you need to 
    /// access individual elements of the DropDown.
    /// </summary>    public class ComboBoxManager : MonoBehaviour, IEnumerable    {        /// <summary>
        /// Where the items will be spawned for the DropDown.
        /// </summary>        public Transform                ItemsContainer;        /// <summary>
        /// Where the current selected item will show up.
        /// </summary>        public Transform                SelectedItemContainer;        /// <summary>
        /// Mask hidding the DropDown. Modifying the mask will reveal or hide the DropDown with the item list.
        /// </summary>        public RectTransform            DropDownMask;        /// <summary>
        /// Prefab used to instantiate each item in the DropDown.
        /// </summary>        public ComboBoxItem             ItemPrefab;        /// <summary>
        /// Prefab used to instantiate the selected item that will be visible when the DropDown is closed.
        /// </summary>        public ComboBoxItem             SelectedItemPrefab;        /// <summary>
        /// How large the DropDown will be when visible (pixels).
        /// </summary>        public int                      DropDownHeight;        /// <summary>
        /// Collection containing the spawned items in the DropDown.
        /// </summary>        protected IList<ComboBoxItem>   m_Items;        /// <summary>
        /// Current selected item. This is the one visible while the DropDown is closed.
        /// </summary>        protected ComboBoxItem          m_SelectedItem;        /// <summary>
        /// Whether the DropDown is visible or not.
        /// </summary>        protected bool                  m_DropDownVisible;

        // ============================ Events ============================= //

        /// <summary>
        /// Represents a ComboBox event related to an item.
        /// </summary>
        /// <param name="text">Text of the item.</param>        public delegate void ComboBoxEvent(string text);        /// <summary>
        /// Event fired when a new item is selected. Subscribe to this event if you want to do some logic everytime the selection changes.
        /// </summary>        public event ComboBoxEvent OnItemSelected;

        // ========================== Properties =========================== //

        /// <summary>
        /// Text of the current selected item.
        /// </summary>        public string SelectedItemText
        {
            get
            {
                return m_SelectedItem.Label.text;
            }
        }

        // ============================ Methods ============================ //

        /// <summary>
        /// Adds a new item to the DropDown.
        /// </summary>
        /// <param name="text">Text the item will display.</param>
        /// <returns>The item spawned in the DropDown to represent the option specified.</returns>
        public ComboBoxItem AddItem(string text)        {            ComboBoxItem item;            item = Utils.Instantiate<ComboBoxItem>(ItemPrefab, ItemsContainer, ItemsContainer.position, ItemsContainer.rotation);

            item.Label.text = text;            m_Items.Add(item);            SubscribeToEvent(item);            return item;        }                /// <summary>
        /// Removes from the DropDown the item with the given text.
        /// </summary>
        /// <param name="text">Text of the item to remove.</param>        public void RemoveItem(string text)        {            ComboBoxItem toRemove;            toRemove = null;            foreach (ComboBoxItem item in m_Items)            {                if (item.Label.text == text)                {                    toRemove = item;                    break;                }            }            if (toRemove != null)            {                UnsubscribeToEvent(toRemove);                m_Items.Remove(toRemove);                Destroy(toRemove.gameObject);            }        }        /// <summary>
        /// Removes all the items in the DropDown.
        /// </summary>        public void Clear()        {            while (m_Items.Count > 0)            {                RemoveItem(m_Items[0].Label.text);            }        }        /// <summary>
        /// If the DropDown is visible, hides it. If is not visible, shows it.
        /// </summary>        public void ToggleDropDown()        {            if (m_DropDownVisible)            {                HideDropDown();            }            else            {                ShowDropDown();            }        }                /// <summary>
        /// Shows the DropDown with all the item elements.
        /// </summary>        public void ShowDropDown()        {            DropDownMask.offsetMin = new Vector2(DropDownMask.offsetMin.x, DropDownMask.offsetMin.y - DropDownHeight);            m_DropDownVisible = true;        }        /// <summary>
        /// Hides the DropDown containing all the item elements. Only the current selected item will be visible.
        /// </summary>        public void HideDropDown()        {            DropDownMask.offsetMin = new Vector2(DropDownMask.offsetMin.x, DropDownMask.offsetMin.y + DropDownHeight);            m_DropDownVisible = false;        }        /// <summary>
        /// Selects the item with the given name.
        /// 
        /// This won't search for elements in the list, but will create the element in the selected item area and send the event,
        /// so you should make sure the text you are sending is a valid option.
        /// </summary>
        /// <param name="text">Text of the element to select.</param>        public void SelectItem(string text)        {            RectTransform rect;            if (m_DropDownVisible)
            {
                HideDropDown();
            }

            if (m_SelectedItem != null)            {                Destroy(m_SelectedItem.gameObject);            }            m_SelectedItem              =   Utils.Instantiate<ComboBoxItem>(SelectedItemPrefab, SelectedItemContainer, SelectedItemContainer.position, SelectedItemContainer.rotation);            m_SelectedItem.Label.text   =   text;            rect                        =   m_SelectedItem.GetComponent<RectTransform>();            rect.sizeDelta              =   SelectedItemContainer.GetComponent<RectTransform>().sizeDelta;            ReportItemSelected(text);        }            private void ReportItemSelected(string text)        {            if (OnItemSelected != null)            {                OnItemSelected(text);            }        }        protected void SubscribeToEvent(ComboBoxItem item)        {            item.OnSelected += SelectItem;        }        protected void UnsubscribeToEvent(ComboBoxItem item)        {            item.OnSelected -= SelectItem;        }

        // ============================= Unity ============================= //

        protected void Start()
        {
            m_Items = new List<ComboBoxItem>();
        }

        // ========================== IEnumerable ========================== //

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Items.GetEnumerator();
        }
    }}