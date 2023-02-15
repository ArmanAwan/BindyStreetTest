
using ArmanDoesStuff.Utilities;
using Bindy.Data;
using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;

namespace Bindy.Display
{
    public class RadialPage : Page
    {
        [SerializeField] GameObject loadingIcon;
        SimpleScrollSnap scrollSnap;
        DynamicContent dynamicContent;
        int firstIndex = -4;
        Sprite[] sprites = new Sprite[20];
        public override void DisplayData()
        {
            StartCoroutine(DisplayData_Croute());
        }

        private void RadialPage_PanelSwitched_Event()
        {
            int posOffCentre = (scrollSnap.NumberOfPanels / 2) - scrollSnap.CenteredPanel;
            while (posOffCentre > 0)
            {
                firstIndex--;
                posOffCentre--;
                int newIndex = ArmanLibrary.LoopInt(firstIndex, 20);
                var g = dynamicContent.AddToFront();
                g.GetComponent<RadialContentBlock>().SetContent(sprites[newIndex], newIndex);
                dynamicContent.RemoveFromBack();
            }
            while (posOffCentre < 0)
            {
                firstIndex++;
                posOffCentre++;
                int newIndex = ArmanLibrary.LoopInt(firstIndex + 6, 20);
                var g = dynamicContent.AddToBack();
                g.GetComponent<RadialContentBlock>().SetContent(sprites[newIndex], newIndex);
                dynamicContent.RemoveFromFront();
            }
        }

        IEnumerator DisplayData_Croute()
        {
            //Get images
            for (int i = 0; i < 20; i++)
            {
                int id = UnityEngine.Random.Range(0, 4000);
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
                    sprites[i] = sprite;
                }
            }

            //Every time the selected panel is changed, perform an action
            scrollSnap = FindObjectOfType<SimpleScrollSnap>();
            scrollSnap.PanelSwitched_Event += RadialPage_PanelSwitched_Event;

            //Create 7 panels by default
            dynamicContent = FindObjectOfType<DynamicContent>();

            for (int i = -4; i <= 4; i++)
            {
                //LoopInt allows us to pass in any index even out of the 0-20 range
                int index = ArmanLibrary.LoopInt(i, 20);
                var g = dynamicContent.AddToBack();
                g.GetComponent<RadialContentBlock>().SetContent(sprites[index], index);

            }
            //Custom invoke that works after a delay, only one frame in this case but it allows the GoToPanel function to work
            yield return null;
            scrollSnap.GoToPanel(4);
            loadingIcon.SetActive(false);
        }
    }
}
