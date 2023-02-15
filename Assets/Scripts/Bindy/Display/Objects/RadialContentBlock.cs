using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bindy.Display
{
    public class RadialContentBlock : MonoBehaviour
    {
        [SerializeField] Image mainImage;
        [SerializeField] TMP_Text mainText;
        public void SetContent(Sprite s, int index)
        {
            mainImage.sprite = s;
            mainText.text = index.ToString();
        }
    }
}
