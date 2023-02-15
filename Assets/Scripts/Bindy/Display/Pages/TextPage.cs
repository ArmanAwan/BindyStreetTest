using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Bindy.Display
{
    public class TextPage : Page
    {
        [SerializeField] RectTransform contentHolder;
        [SerializeField] ScrollRect scrollRect;
        //2 different templates (left aligned image vs right aligned image
        [SerializeField] GameObject[] templates;
        const int itemsPerPage = 20;
        int itemNum = 1;

        public override void DisplayData()
        {
            Debug.Log("loading new page");
            //Populate a new page with 20 random items
            int[] contentIDs = new int[itemsPerPage];
            for (int i = 0; i < contentIDs.Length; i++)
                contentIDs[i] = Random.Range(0, 4000);
            List<GameObject> returnList = new List<GameObject>();
            //switch which template to build on each iteration
            bool templateType = false;
            for (int i = 0; i < contentIDs.Length; i++)
            {
                templateType = !templateType;
                //create it and add it to the return list
                GameObject g = Instantiate(templateType ? templates[0] : templates[1], contentHolder);
                g.GetComponent<ContentBlock>().SetContentFromID(contentIDs[i], itemNum);
                itemNum++;
            }
            //rescale/position holder
            scrollRect.enabled = false; //have to disable to break touches, otherwise scrollrect will jump afterwards
            var startPosY = contentHolder.localPosition.y;
            var startSize = contentHolder.sizeDelta.y;
            //set size based on number of items
            //currently just doing it by eye (6.5 items fit in the screen) but can make it more precise if using the prefab size
            contentHolder.sizeDelta = new Vector2(contentHolder.sizeDelta.x, Screen.height * (contentHolder.childCount / 6.5f));
            //move up by half the added height to maintain position
            contentHolder.localPosition = new Vector2(contentHolder.localPosition.x, startPosY - ((contentHolder.sizeDelta.y - startSize) * 0.5f));
            scrollRect.enabled = true;
        }

        IEnumerator DisplayData_Croute()
        {
            Debug.Log("loading new page");
            scrollRect.enabled = false;
            //Populate a new page with 20 random items
            int[] contentIDs = new int[itemsPerPage];
            for (int i = 0; i < contentIDs.Length; i++)
                contentIDs[i] = Random.Range(0, 4000);
            List<GameObject> returnList = new List<GameObject>();
            //switch which template to build on each iteration
            bool templateType = false;
            for (int i = 0; i < contentIDs.Length; i++)
            {
                templateType = !templateType;
                //create it and add it to the return list
                GameObject g = Instantiate(templateType ? templates[0] : templates[1], contentHolder);
                g.GetComponent<ContentBlock>().SetContentFromID(contentIDs[i], itemNum);
                itemNum++;
            }
            //rescale/position holder
            var startPosY = contentHolder.localPosition.y;
            var startSize = contentHolder.sizeDelta.y;
            contentHolder.sizeDelta = new Vector2(contentHolder.sizeDelta.x, Screen.height * (contentHolder.childCount / 6.5f));
            yield return null;
            contentHolder.localPosition = new Vector2(contentHolder.localPosition.x, startPosY - ((contentHolder.sizeDelta.y - startSize) * 0.5f));
            scrollRect.enabled = true;
        }

        private void Update()
        {
            if (scrollRect.verticalNormalizedPosition <= 0f)
                DisplayData();
        }
    }
}
