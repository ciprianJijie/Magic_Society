using UnityEngine;

namespace MS.Controllers.UI
{
    public class ResourceAmountController : Controller<Views.UI.ResourceAmountView, Model.ResourceAmount>
    {
        public void Show(Model.ResourceAdvancedAmount advancedAmount)
        {
            foreach (Model.ResourceAmount amount in advancedAmount)
            {
                var view = CreateView(amount);
                view.UpdateView(amount);
            }

            // Calculate position
            Vector2 mousePosition;
            Vector2 offset;
            Vector2 position;
            RectTransform rect;

            rect = Holder.GetComponent<RectTransform>();
            mousePosition = Input.mousePosition;
            offset.x = rect.rect.width / 2f;
            offset.y = rect.rect.height / 2f;

            position = mousePosition + offset;

            rect.anchoredPosition = position;

            Holder.gameObject.SetActive(true);
        }

        public void Hide()
        {
            ClearViews();
            Holder.gameObject.SetActive(false);
        }

        protected void Start()
        {
            Holder.gameObject.SetActive(false);
        }
    }
}
