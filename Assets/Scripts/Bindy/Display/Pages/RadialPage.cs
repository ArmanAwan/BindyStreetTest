
using ArmanDoesStuff.Utilities;
using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Bindy.Display
{
    public class RadialPage : Page
    {
        [SerializeField] GameObject loadingIcon;
        SimpleScrollSnap scrollSnap;
        ScrollRect scrollRect;
        DynamicContent dynamicContent;
        int firstIndex;
        public static Sprite[] sprites = new Sprite[20];
        const int arraySize = 17;
        public override void DisplayData()
        {
            StartCoroutine(DisplayData_Croute());
        }

        IEnumerator DisplayData_Croute()
        {
            //Every time the selected panel is changed, perform an action
            scrollRect = FindObjectOfType<ScrollRect>();
            scrollSnap = FindObjectOfType<SimpleScrollSnap>();
            scrollSnap.PanelSwitched_Event += RadialPage_PanelSwitched_Event;

            //Create 7 panels by default
            dynamicContent = FindObjectOfType<DynamicContent>();

            firstIndex = -arraySize / 2;
            for (int i = firstIndex; i <= firstIndex * -1; i++)
            {
                //LoopInt allows us to pass in any index even out of the 0-20 range
                int index = ArmanLibrary.LoopInt(i, 20);
                var g = dynamicContent.AddToBack();
                g.GetComponent<RadialContentBlock>().SetContent(index);

            }
            //Custom invoke that works after a delay, only one frame in this case but it allows the GoToPanel function to work
            yield return null;
            scrollSnap.GoToPanel(arraySize / 2);
            loadingIcon.SetActive(false);
        }

        private void RadialPage_PanelSwitched_Event()
        {
            //how far from the middle pannel the user has scrolled to
            int posOffCentre = (scrollSnap.NumberOfPanels / 2) - scrollSnap.CenteredPanel;
            //add a panel to the left and remove right untill centered
            while (posOffCentre > 0)
            {
                firstIndex--;
                posOffCentre--;
                int newIndex = ArmanLibrary.LoopInt(firstIndex, 20);
                var g = dynamicContent.AddToFront();
                g.GetComponent<RadialContentBlock>().SetContent(newIndex);
                dynamicContent.RemoveFromBack();
            }
            while (posOffCentre < 0) //opposite of above
            {
                firstIndex++;
                posOffCentre++;
                int newIndex = ArmanLibrary.LoopInt(firstIndex + arraySize, 20);
                var g = dynamicContent.AddToBack();
                g.GetComponent<RadialContentBlock>().SetContent(newIndex);
                dynamicContent.RemoveFromFront();
            }
        }
        private void FixedUpdate()
        {
            if (scrollRect.horizontalNormalizedPosition < 0.1f || scrollRect.horizontalNormalizedPosition > .9f)
                scrollRect.enabled = false;
            else
                scrollRect.enabled = true;
        }
    }
}
