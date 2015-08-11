using UnityEngine;
using UnityEngine.UI;

namespace MS
{
    public class SelectFileWindowManager : MonoBehaviour
    {
        public string DirectoryPath;

        public SelectFileWindowSingleFileManager SingleFilePrefab;

        public ToggleGroup ToggleGroup;

        // Events
        public delegate void FileSelectionEvent(string file);

        public static void DefaultAction(string file) {}

        public event FileSelectionEvent OnFileSelected = DefaultAction;
        // ---

        public void TriggerOnFileSelectedEvent()
        {
            foreach (Toggle toggle in ToggleGroup.ActiveToggles())
            {
                OnFileSelected(toggle.GetComponent<SelectFileWindowSingleFileManager>().FilePath);
            }
        }

        protected void Start()
        {
            string      directoryFullPath;
            string[]    filesInDirectory;

            directoryFullPath   =   Application.streamingAssetsPath + DirectoryPath;
            filesInDirectory    =   System.IO.Directory.GetFiles(directoryFullPath, "*.json");

            foreach (string filePath in filesInDirectory)
            {
                var toggle          =   Utils.Instantiate<SelectFileWindowSingleFileManager>(SingleFilePrefab, ToggleGroup.transform, this.transform.position, this.transform.rotation);
                toggle.FilePath     =   filePath;
                toggle.ToggleGroup  =   ToggleGroup;

                int lastSlashIndex;

                lastSlashIndex = filePath.LastIndexOf("/");

                toggle.LabelText    =   filePath.Substring(lastSlashIndex + 1);
            }
        }
    }
}
