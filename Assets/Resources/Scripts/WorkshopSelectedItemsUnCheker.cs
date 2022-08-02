using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopSelectedItemsUnCheker : MonoBehaviour
{
    const string NameOKIconItem = "OKIconItem";

    [SerializeField] internal List<Transform> workshopitems = new List<Transform>();
   

    internal void ClearList()
    {
        workshopitems = new List<Transform>();
    }

    internal void AddedListItems()
    {
        for(int i = 0; i < transform.childCount; i++)
            workshopitems.Add(transform.GetChild(i));
    }
    internal void UnCheckOK(Transform checkOn)
    {
       for(int i = 0; i < transform.childCount; i++) 
       { 
            Debug.Log(transform.GetChild(i));
            if(transform.GetChild(i) != checkOn)
                transform.GetChild(i).Find(NameOKIconItem).GetComponent<Image>().enabled = false;
       }
    }
}
