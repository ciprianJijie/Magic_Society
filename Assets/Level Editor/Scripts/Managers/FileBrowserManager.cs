using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace MS
{
	public class FileBrowserManager : MonoBehaviour
	{
		protected void Start()
		{
            string directoryPath;
            string[] files;

            directoryPath 	= 	Application.streamingAssetsPath + "/Maps/";
            files  			= 	Directory.GetFiles(directoryPath, "*.json");

            foreach (string file in files)
			{
                GameObject fileButtonObj;
                FileButtonManager fileButton;
                Button button;
                string shortName;
                int indexLastSlash;
                int indexLastDot;

                fileButtonObj 	= 	Instantiate(FileButtonPrefab.gameObject);
                fileButton 		= 	fileButtonObj.GetComponent<FileButtonManager>();
                button 			= 	fileButtonObj.GetComponent<Button>();

                fileButtonObj.transform.SetParent(FileButtonsContainer, false);

                indexLastSlash 		= 	file.LastIndexOf("/");
                indexLastDot 		= 	file.LastIndexOf(".");
                shortName 			= 	file.Substring(indexLastSlash + 1, indexLastDot - indexLastSlash - 1);
                fileButton.Text 	= 	shortName;
				fileButton.FilePath = 	file;

				button.onClick.AddListener(() => { SelectFile(fileButton);  } );
            }
        }

		protected void SelectFile(FileButtonManager button)
		{
            LevelEditorManager.Instance.Load(button.FilePath);
            Close();
        }

		public void Close()
		{
            Destroy(this.gameObject);
        }


        public FileButtonManager FileButtonPrefab;
        public Transform FileButtonsContainer;
    }
}
