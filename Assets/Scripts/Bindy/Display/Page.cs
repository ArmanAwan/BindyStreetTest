using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace Bindy.Display
{
    public abstract class Page : MonoBehaviour
    {
        public string header;
        private void Awake()
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }
        public virtual void DisplayData()
        {
            Debug.LogError("Trying to Get Data from Base");
            throw new System.NotImplementedException();
        }
    }
}
