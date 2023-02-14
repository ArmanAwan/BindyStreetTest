using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bindy.Display
{
    public class TextPage : Page
    {
        //2 different templates (left aligned image vs right aligned image
        public GameObject[] templates;
        int[] contentIDs;

        //Returns all the blocks/GameObjects to the display
        public override GameObject[] DisplayData()
        {
            List<GameObject> returnList = new List<GameObject>();
            bool templateType = false;
            for (int i = 0; i < contentIDs.Length; i++)
            {
                //switch which template to build
                templateType = !templateType;
                //create it and add it to the return list
                GameObject g = Instantiate(templateType ? templates[0] : templates[1]);
            }
            return returnList.ToArray();
        }
    }
}
