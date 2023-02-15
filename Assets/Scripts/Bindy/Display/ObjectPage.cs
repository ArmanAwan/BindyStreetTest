using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.CM.Triggers.TriggerRunner;

namespace Bindy.Display
{
    public class ObjectPage : Page
    {
        Animator animator;
        int animHash;
        public override void DisplayData()
        {
            animator = FindObjectOfType<Animator>();
            animHash = Animator.StringToHash("playAnim");
            //Fill all the content blocks in the scene
            int itemNum = 0;
            foreach (var item in FindObjectsOfType<ContentBlock>())
            {
                itemNum++;
                item.SetContentFromID(Random.Range(0, 4000), itemNum);
            }
        }

        public void StartAnimation_Btn()
        {
            //Button that covers the 3D model, when clicked the animation will begin to play
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("mainAnim"))
                animator.SetTrigger(animHash);
        }
    }
}
