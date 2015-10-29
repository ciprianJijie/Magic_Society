using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MS
{
    public class MenuManager : MonoBehaviour
    {
		public Button NewGameButton;
		public Button LoadGameButton;
		public Button LevelEditorButton;
		public Button ExitButton;

        public SelectFileWindowManager SelectFileWindow;

        public void LoadScene(string name)
        {
            Application.LoadLevel(name);
        }

        protected void OnNewGame(string mapFilePath)
        {
            Model.Game.Instance.New(3, 1);

            LoadScene("Main");
        }

        // UI Interactions

        public void ShowNewGameWindow()
        {
            SelectFileWindow.gameObject.SetActive(true);
        }

        public void HideNewGameWindow()
        {
            SelectFileWindow.gameObject.SetActive(false);
        }

		public void Exit()
		{
			Application.Quit();
		}

        // Unity Methods

        protected void Start()
        {
            SelectFileWindow.OnFileSelected += OnNewGame;

			LoadGameButton.interactable = false;
			LevelEditorButton.interactable = false;
        }

        protected void OnDestroy()
        {
            SelectFileWindow.OnFileSelected -= OnNewGame;
        }

    }
}
