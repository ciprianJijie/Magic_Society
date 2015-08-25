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
            int lastSlashIndex;
            int lastDotIndex;
            string mapName;

            mapFilePath = Path.PlatformPath(mapFilePath);

#if UNITY_STANDALONE_WIN
            lastSlashIndex = mapFilePath.LastIndexOf(@"\");
#else
            lastSlashIndex = mapFilePath.LastIndexOf("/");
#endif

            lastDotIndex = mapFilePath.LastIndexOf(".");
            mapName = mapFilePath.Substring(lastSlashIndex + 1, lastDotIndex - lastSlashIndex - 1);

            GameController.Instance.Game.New(mapName, 2, 1);

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
