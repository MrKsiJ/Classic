using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

namespace GracesGames.SimpleFileBrowser.Scripts.UI {

    public class FileButton : MonoBehaviour, IPointerClickHandler {

        // The file browser using this file button
        private FileBrowser _fileBrowser;

        // The path of the button
        private string _path = "";

        // Whether the button is interactable
        private bool _interactable;

        // click and double click variables
        private int _clickCount;
        private float _firstClickTime;
        private float _currentTime;
        // Change this constant to tweak the time between single and double clicks
        private const float DoubleClickInterval = 0.25f;

        // Set variables, called by UserInterface script
        public void Setup(FileBrowser fileBrowser, string path, bool interactable) {
            _fileBrowser = fileBrowser;
            _path = path;
            _interactable = interactable;
        }

        // When single clicked, call FileClick method
        // When double clicked, call FileClick and SelectFile method
        public void OnPointerClick(PointerEventData eventData) {
            if (_interactable) {
                _clickCount++;
            }
            
            if (_clickCount != 1) return;
            _firstClickTime = eventData.clickTime;
            _currentTime = _firstClickTime;
            StartCoroutine(ClickRoutine());
        }

        private IEnumerator ClickRoutine() {
            while (_clickCount != 0) {
                yield return new WaitForEndOfFrame();

                _currentTime += Time.deltaTime;

                if (!(_currentTime > _firstClickTime + DoubleClickInterval)) continue;
                if (_clickCount == 1) {
                    _fileBrowser.FileClick(_path);
                    if(!_fileBrowser.addWorkItem.isSearchFileInAddWorkshopItem)
                        Camera.main.GetComponent<MenuGameController>().textMyMusicSelected.text = GetComponent<Text>().text;
                    else
                    {
                        if (!_fileBrowser.addWorkItem.isAWPSkin)
                        {
                            _fileBrowser.addWorkItem.FileNameText.text = GetComponent<Text>().text;
                            _fileBrowser.addWorkItem.isSelectedFileWorkshop = true;
                        }
                        else
                        {
                            _fileBrowser.addWorkItem.IconNameText.text = GetComponent<Text>().text;
                            _fileBrowser.addWorkItem.isSelectedIconWorkshop = true;
                        }
                            
                    }
                    _fileBrowser.SelectFile();
                    _fileBrowser.CloseFileBrowser();
                } else {
                    _fileBrowser.FileClick(_path);
                    if (!_fileBrowser.addWorkItem.isSearchFileInAddWorkshopItem)
                        Camera.main.GetComponent<MenuGameController>().textMyMusicSelected.text = GetComponent<Text>().text;
                    else
                    {
                        if (!_fileBrowser.addWorkItem.isAWPSkin)
                        {
                            _fileBrowser.addWorkItem.FileNameText.text = GetComponent<Text>().text;
                            _fileBrowser.addWorkItem.isSelectedFileWorkshop = true;
                        }
                        else
                        {
                            _fileBrowser.addWorkItem.IconNameText.text = GetComponent<Text>().text;
                            _fileBrowser.addWorkItem.isSelectedIconWorkshop = true;
                        }
                    }
                    _fileBrowser.SelectFile();
                    _fileBrowser.CloseFileBrowser();
                }

                _clickCount = 0;
            }
        }
    }
}
