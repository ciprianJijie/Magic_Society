using UnityEngine;
using System.Collections;
using MS.Views.Editor.UI;

namespace MS.Editor.UI
{
    public class ObjectInspectorManager : MonoBehaviour
    {
        public MapElementView           ElementView;
        public OwnableMapElementView    OwnableView;

        public void Show(Model.MapElement element)
        {
            ElementView.BindTo(element);
            ElementView.UpdateView(element);

            if (element is Model.OwnableMapElement)
            {
                OwnableView.gameObject.SetActive(true);
                OwnableView.BindTo(element as Model.OwnableMapElement);
                OwnableView.UpdateView();
                //OwnableView.UpdateView(element as Model.OwnableMapElement);

                // Subscribe to the selection of a new owner
                OwnableView.ComboBox.OnItemSelected += OnOwnerChanged;
            }
            else
            {
                OwnableView.gameObject.SetActive(false);

                // Unsubscribe to the selection of a new owner
                OwnableView.ComboBox.OnItemSelected -= OnOwnerChanged;
            }
        }

        protected void OnOwnerChanged(string ownerName)
        {
            OwnableView.Model.Owner = Managers.GameManager.Instance.Game.Players.Find(ownerName);
        }
    }
}
