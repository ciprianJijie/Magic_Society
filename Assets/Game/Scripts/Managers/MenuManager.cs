using UnityEngine;
using System.Collections;

namespace MS
{
    public class MenuManager : MonoBehaviour
    {
        public SelectFileWindowManager SelectFileWindow;

        public void LoadScene(string name)
        {
            Application.LoadLevel(name);
        }

        protected void OnNewGame(string mapFilePath)
        {
            Managers.GameManager.Instance.Game.New(3, 1);

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

        // Unity Methods

        protected void Start()
        {
            SelectFileWindow.OnFileSelected += OnNewGame;
        }

        protected void OnDestroy()
        {
            SelectFileWindow.OnFileSelected -= OnNewGame;
        }

    }
}
