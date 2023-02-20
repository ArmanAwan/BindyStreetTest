using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
//using Newtonsoft.Json;

namespace Bindy.Data
{
    public static class DataManager
    {
        public class jsonPlaceholderKey
        {
            public int albumId;
            public int id;
            public string title;
            public string url;
            public string thumbnailUrl;
        }
        public static jsonPlaceholderKey[] jsonData;

        public static IEnumerator LoadData_Croute()
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
                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //jsonData = js.Deserialize<jsonPlaceholderKey[]>(request.downloadHandler.text);

                    //jsonData = JsonConvert.DeserializeObject<jsonPlaceholderKey>(request.downloadHandler.text);
                }
            }
        }
    }
}
