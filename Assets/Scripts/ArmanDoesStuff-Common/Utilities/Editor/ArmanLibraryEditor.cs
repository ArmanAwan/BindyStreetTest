using UnityEditor;
using UnityEngine;

namespace ArmanDoesStuff.Utilities
{
    public class ArmanLibraryEditor : MonoBehaviour
    {
        [MenuItem("Arman Library/Anchors to Corners %[")]
        static void Shortcut_AnchorToCorner()
        {
            foreach (Transform t in Selection.transforms)
            {
                RectTransform rt = t.GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.AnchorToCorner();
                }
            }
        }

        [MenuItem("Arman Library/Corners to Anchors %]")]
        static void Shortcut_CornerToAnchor()
        {
            foreach (Transform t in Selection.transforms)
            {
                RectTransform rt = t.GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.CornerToAnchor();
                }
            }
        }
    }
}