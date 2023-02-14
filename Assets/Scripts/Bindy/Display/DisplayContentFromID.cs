using Bindy.Data;
using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using TMPro;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Bindy.Display
{
    public class DisplayContentFromID : MonoBehaviour
    {
        public IEnumerator GetImageFromID_Croute(int id)
        {
            //Get the image url based on the index
            DataManager dataManager = FindObjectOfType<DataManager>();
            Debug.Log(dataManager.jsonData[id].thumbnailUrl);
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(dataManager.jsonData[id].thumbnailUrl);
            //Request and error check
            yield return request.SendWebRequest();
            if (request.error != null)
                Debug.LogError(request.error);
            else //If no error, set image
            {
                //Get texture
                var tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                //Create Sprite from texture
                Sprite sprite = Sprite.Create(tex,
                new Rect(0, 0, tex.width, tex.height),
                Vector2.one / 2);
                //Set sprite
                GetComponentInChildren<Image>().sprite = sprite;
            }
            //Set text
            GetComponentInChildren<TMP_Text>().text = dataManager.jsonData[id].title;
        }
    }
}
