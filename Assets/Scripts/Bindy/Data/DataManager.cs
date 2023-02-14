using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using UnityEngine;
using UnityEngine.Networking;

namespace Bindy.Data
{
    public class DataManager : MonoBehaviour
    {
        public class jsonPlaceholderKey
        {
            public int albumId;
            public int id;
            public string title;
            public string url;
            public string thumbnailUrl;
        }
        public jsonPlaceholderKey[] jsonData;

        void Start()
        {
            StartCoroutine(LoadFirstPage());
        }

        public IEnumerator LoadFirstPage()
        {
            //Populate jsonData
            //Create UnityWebRequest with jsonplaceholder images 
            using (UnityWebRequest request = UnityWebRequest.Get("https://jsonplaceholder.typicode.com/photos"))
            {
                //send request and error check
                yield return request.SendWebRequest();
                if (request.error != null)
                {
                    Debug.LogError(request.error);
                }
                else if (request.isDone) //if none are found, deserialize into jsonPlaceholderKey class
                {
                    Debug.Log(request.downloadHandler.text);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    jsonData = js.Deserialize<jsonPlaceholderKey[]>(request.downloadHandler.text);
                }
            }
            //Load Page 1
        }
    }
}
