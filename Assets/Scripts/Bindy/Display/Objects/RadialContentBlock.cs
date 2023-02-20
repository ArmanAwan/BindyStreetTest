using Bindy.Data;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Bindy.Display
{
    public class RadialContentBlock : MonoBehaviour
    {
        [SerializeField] Image mainImage;
        [SerializeField] TMP_Text mainText;
        [SerializeField] RectTransform mainRectTransform;
        public void SetContent(int index)
        {
            if (RadialPage.sprites[index] != null)
                mainImage.sprite = RadialPage.sprites[index];
            else
                StartCoroutine(SetContent_Croute(index));
            mainText.text = index.ToString();
        }

        IEnumerator SetContent_Croute(int index)
        {
            int id = UnityEngine.Random.Range(0, 4000);
            //Get the image url based on the index
            if (DataManager.jsonData == null)
                yield break;
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
                RadialPage.sprites[index] = sprite;
                mainImage.sprite = RadialPage.sprites[index];
            }
        }

        private void FixedUpdate()
        {
            float xPos = Mathf.Abs(transform.position.x) / 3f;
            var size = (1 - xPos) + 0.2f;
            mainRectTransform.localScale = new Vector2(size, size);
            mainRectTransform.position = new Vector2(mainRectTransform.position.x, -xPos);

        }
    }
}
