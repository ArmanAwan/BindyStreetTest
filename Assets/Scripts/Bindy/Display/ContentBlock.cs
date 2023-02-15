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
    public class ContentBlock : MonoBehaviour
    {
        [SerializeField] Image mainImage;
        [SerializeField] TMP_Text mainText;
        public void SetContentFromID(int id, int itemNum)
        {
            StartCoroutine(SetContentFromID_Croute(id, itemNum));
        }
        IEnumerator SetContentFromID_Croute(int id, int itemNum)
        {
            //Get the image url based on the index
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(DataManager.jsonData[id].thumbnailUrl);
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
                mainImage.sprite = sprite;
            }
            //Set text
            mainText.text = $"{itemNum} - {DataManager.jsonData[id].title}";
        }
    }
}
