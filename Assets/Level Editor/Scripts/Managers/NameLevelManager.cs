using UnityEngine;
using UnityEngine.UI;

namespace MS
{
	public class NameLevelManager : MonoBehaviour
	{
		public void OnCancelButton()
		{
            Close();
        }

		public void OnCreateButton()
		{
			LevelEditorManager.Instance.New(NameInput.text, 2, 2);
            Close();
        }

		private void Close()
		{
            Destroy(this.gameObject);
        }

        public InputField NameInput;
    }
}
