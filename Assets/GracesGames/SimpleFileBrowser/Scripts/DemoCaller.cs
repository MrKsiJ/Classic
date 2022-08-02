using UnityEngine;

namespace GracesGames.SimpleFileBrowser.Scripts 
{
	// Demo class to illustrate the usage of the FileBrowser script
	// Able to save and load files containing serialized data (e.g. text)
	public class DemoCaller : MonoBehaviour 
    {

		// Use the file browser prefab
		public GameObject FileBrowserPrefab;

        public AddWorkShopItem CreateWorkUI;

		// Define a file extension
		public string[] FileExtensions;

		// Open a file browser to save and load files
		public void OpenFileBrowser() {
            FileBrowserPrefab.GetComponent<FileBrowser>().addWorkItem = CreateWorkUI;
            // Create the file browser and name it
            GameObject fileBrowserObject = Instantiate(FileBrowserPrefab, transform);
			fileBrowserObject.name = "FileBrowser";
			// Set the mode to save or load
			FileBrowser fileBrowserScript = fileBrowserObject.GetComponent<FileBrowser>();
			fileBrowserScript.SetupFileBrowser(ViewMode.Portrait);
	        fileBrowserScript.OpenFilePanel(FileExtensions);
            fileBrowserScript.OnFileSelect += LoadFileUsingPath;


        }

		// Loads a file using a path
		private void LoadFileUsingPath(string path) 
        {
            if (CreateWorkUI.isSearchFileInAddWorkshopItem)
            {
                if (path.Length != 0)
                {
                    if (!CreateWorkUI.isAWPSkin)
                        CreateWorkUI.GetComponent<AddWorkShopItem>().pathToFile = path;
                    else
                        CreateWorkUI.GetComponent<AddWorkShopItem>().pathToIcon = path;
                }
                else
                {
                    if (!CreateWorkUI.isAWPSkin)
                        CreateWorkUI.GetComponent<AddWorkShopItem>().pathToFile = "";
                    else
                        CreateWorkUI.GetComponent<AddWorkShopItem>().pathToIcon = "";
                }
                CreateWorkUI.isSearchFileInAddWorkshopItem = false;
            }
            else
            {
                if (path.Length != 0)
                    GetComponent<MenuGameController>().PathToMusic = path;
                else
                    GetComponent<MenuGameController>().PathToMusic = "";
            }
        }
       
	}
}