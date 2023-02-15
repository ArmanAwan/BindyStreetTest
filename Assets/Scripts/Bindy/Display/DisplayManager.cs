using Bindy.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bindy.Display
{
    public class DisplayManager : MonoBehaviour
    {
        [SerializeField] TMP_Text header;
        void Start()
        {
            //Populate the data (Done once at the start and stored, can change this to save memory)
            StartCoroutine(DataManager.LoadData_Croute());
            StartCoroutine(LoadFirstPage_Croute());
        }

        IEnumerator LoadFirstPage_Croute()
        {
            //Waits until data is loaded. Can have a fallback here (show error/try again)
            //in case it fails to load, but I've ignored that possibility for the test
            while (DataManager.jsonData == null)
                yield return null;
            LoadPage(2); //Load first page
        }

        public void LoadPage(int index)
        {
            //Make sure only one page is active
            Transform pageHolder = GameObject.Find("Canvas/CurrentPage").transform;
            foreach (Transform t in pageHolder)
            {
                Destroy(t.gameObject);
            }
            //Gets the page from Resources to save memory/load time. Could also use Addressables
            Page[] pages = Resources.LoadAll<Page>("Pages");
            var newPage = Instantiate(pages[index], pageHolder);
            header.text = newPage.header;
            newPage.DisplayData();
        }
    }
}