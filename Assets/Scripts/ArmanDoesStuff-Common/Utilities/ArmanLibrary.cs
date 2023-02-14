//ArmanDoesStuff 2017
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ArmanDoesStuff.Utilities
{
    public static class ArmanLibrary
    {
        //Exit the game
        public static void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        //Get an array of the scenes in the build (starting from an index)
        public static string[] GetScenes(int startIndex = 0)
        {
            int sCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            string[] sList = new string[sCount - startIndex];
            for (int i = 0; i < sList.Length; i++)
            {
                sList[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i + startIndex));
            }
            return sList;
        }

        //Random boolean based on chance. Note: May need to change Random.InitState before use
        public static bool RandomChance(float chance)
        {
            return UnityEngine.Random.Range(0f, 1f) < chance;
        }

        //Random Rotate
        public static void RotateRandom(this Transform t)
        {
            t.rotation = UnityEngine.Random.rotation;
        }

        public static void RotateRandom(this Transform t, float _spread)
        {
            _spread /= 2;
            t.Rotate(RandomVec3(_spread));
        }

        public static Vector3 RandomVec3(float _spread = 180)
        {
            return new Vector3(UnityEngine.Random.Range(-_spread, _spread), UnityEngine.Random.Range(-_spread, _spread), UnityEngine.Random.Range(-_spread, _spread));
        }

        public static void RotateRandomPerlin(this Transform t, float _seed = 0, float _timeScale = 0)
        {
            t.Rotate(RandomPerlinVec3(_seed, _timeScale));
        }

        //Random Perlin Float - Between 0 & 1
        public static float RandomPerlinFloat(float _seed = 0, float _timeScale = 1)
        {
            return Mathf.PerlinNoise(_seed, _seed + (Time.time * _timeScale));
        }

        //Random Perlin Float - Between -1 & 1
        public static float RandomPerlinFloatBalanced(float _seed = 0, float _timeScale = 1)
        {
            return (RandomPerlinFloat(_seed, _timeScale) - 0.5f) * 2f;
        }

        //Random Perlin vec3 - Between 0 & 1
        public static Vector3 RandomPerlinVec3(float _seed = 0, float _timeScale = 1)
        {
            return new Vector3(
                RandomPerlinFloat(_seed, _timeScale),
                RandomPerlinFloat(_seed + 1000, _timeScale),
                RandomPerlinFloat(_seed + 1000000, _timeScale)
                );
        }

        //Random Perlin vec3 - Between -1 & 1
        public static Vector3 RandomPerlinVec3Balanced(float _seed = 0, float _timeScale = 1)
        {
            return new Vector3(
                RandomPerlinFloatBalanced(_seed, _timeScale),
                RandomPerlinFloatBalanced(_seed + 1000, _timeScale),
                RandomPerlinFloatBalanced(_seed + 1000000, _timeScale)
                );
        }

        //Check if a rect contains another rect
        public static bool Contains(this RectTransform self, RectTransform rect)
        {
            Vector3[] selfCorners = new Vector3[4];
            self.GetWorldCorners(selfCorners);

            Vector3[] objectCorners = new Vector3[4];
            rect.GetWorldCorners(objectCorners);

            return
                selfCorners[0].x <= objectCorners[0].x //bottom left corner
                && selfCorners[0].y <= objectCorners[0].y
                && selfCorners[2].x >= objectCorners[2].x //top right corner
                && selfCorners[2].y >= objectCorners[2].y
                ;
        }

        //Float to Vector3
        public static Vector3 ToVector3(this float f)
        {
            return new Vector3(f, f, f);
        }

        //Vector3[] to Vector2[]
        public static Vector2[] ToVector2Array(this Vector3[] v3)
        {
            return System.Array.ConvertAll<Vector3, Vector2>(v3, getV3fromV2);
        }

        public static Vector2 getV3fromV2(Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }

        //Float to Color
        public static Color ToColor(this float f, float a = 1)
        {
            return new Color(f, f, f, a);
        }

        //Random signed integer, to randomly make something positive/negative
        public static int RandomSign()
        {
            return (UnityEngine.Random.Range(0, 2) * 2) - 1;
        }

        //Random boolean, usefull for decision simple making
        public static bool RandomBoolean()
        {
            return UnityEngine.Random.Range(0f, 1f) > 0.5 ? true : false;
        }

        //bool to int
        public static int ToInt(this bool b, int mult = 1)
        {
            return (b ? 1 : 0) * mult;
        }

        //bool to int with different outputs
        public static int ToIntNegative(this bool b, int mult = 1)
        {
            return (b ? 1 : -1) * mult;
        }

        //Wraps an int, both directions
        public static int LoopInt(this int i, int count)
        {
            if (count <= 0)
            {
                Debug.LogError("LoopInt Count less than or equal to 0");
                return 0;
            }

            while (i < 0)
            {
                i += count;
            }
            return i % count;
        }

        //Random choice of multiple different options/weights
        public static int RandomChoice(List<float> weights)
        {
            //get total of all weights and then choose a random one
            float weight = 0;
            foreach (float f in weights)
            {
                weight += f;
            }
            float chance = UnityEngine.Random.Range(0, weight);

            //return the random one by cycling through until it is reached
            weight = 0; //recycle variable
            for (int i = 0; i < weights.Count; i++)
            {
                weight += weights[i]; //same operation as before be written differently so I can return the actual choice (increment value)
                if (weight > chance)
                {
                    return i;
                }
            }
            return 0;
        }

        //Angle between two points, good for setting objective markers
        public static float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        //Round to nearest multiple of a number
        public static float RoundToNearestMultiple(this float numberToRound, float multipleOf)
        {
            int multiple = Mathf.RoundToInt(numberToRound / multipleOf);
            return multiple * multipleOf;
        }

        //Check if string only contains digits
        public static bool IsDigitsOnly(this string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }

        //Hexedecimal To RGBA
        public static Color HexToColor(this string hex, float alpha = 1)
        {
            hex = hex.Trim(new char[] { ' ', '#' });
            if (hex.Length != 6)
            {
                return new Color(0, 0, 0, 1);
            }
            else
            {
                return new Color(HexToFloat(hex.Substring(0, 2)), HexToFloat(hex.Substring(2, 2)), HexToFloat(hex.Substring(4, 2)), Mathf.Clamp01(alpha));
            }
        }

        //Hexedecimal To Float
        public static float HexToFloat(string hex)
        {
            return ((float)int.Parse(hex, System.Globalization.NumberStyles.HexNumber)) / 256f;
        }

        public static Vector3[] Push(this Vector3[] pArray, Vector3 p) //returns an array with the pushed value //find a way to change directly, do pointers work in unity?
        {
            for (int i = 1; i < pArray.Length; i++)
            {
                pArray[i] = pArray[i - 1];
            }
            pArray[0] = p;
            return pArray;
        }

        //UI - Place Anchors at Corners
        public static void AnchorToCorner(this RectTransform t)
        {
            RectTransform tP = t.parent as RectTransform;

            Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / tP.rect.width,
                                                    t.anchorMin.y + t.offsetMin.y / tP.rect.height);
            Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / tP.rect.width,
                                                t.anchorMax.y + t.offsetMax.y / tP.rect.height);
            t.anchorMin = newAnchorsMin;
            t.anchorMax = newAnchorsMax;

            t.offsetMin = t.offsetMax = new Vector2(0, 0);
        }

        //UI - Place Anchors at Canvas Corners
        public static void AnchorToCanvas(this RectTransform t)
        {
            t.anchorMin = new Vector2(0, 0);
            t.anchorMax = new Vector2(1, 1);
        }

        public static void CornerToAnchor(this RectTransform t)
        {
            t.offsetMin = t.offsetMax = new Vector2(0, 0);
        }

        public static string TimeToString(System.TimeSpan t)
        {
            string ret = "";

            int TimeUnitsToDisplay = 0;
            if (t.TotalDays > 0) { TimeUnitsToDisplay = 3; }
            else if (t.TotalHours > 0) { TimeUnitsToDisplay = 2; }
            else if (t.TotalMinutes > 0) { TimeUnitsToDisplay = 1; }

            switch (TimeUnitsToDisplay)
            {
                case 3:
                    ret += t.Days + " days, ";
                    goto case 2;
                case 2:
                    ret += t.Hours + " hours, ";
                    goto case 1;
                case 1:
                    ret += t.Minutes + " minutes and ";
                    goto case 0;
                case 0:
                    ret += t.Seconds + " seconds";
                    break;
                default:
                    break;
            }

            return ret;
        }

        //Fills array with value
        public static void Populate<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i] = value;
            }
        }

        //Invoke Lambda Expression (next frame or with delay)
        //https://forum.unity.com/threads/tip-invoke-any-function-with-delay-also-with-parameters.978273/
        public static void Invoke(this MonoBehaviour mb, Action f, float delay = 0)
        {
            mb.StartCoroutine(InvokeRoutine(f, delay));
        }

        private static IEnumerator InvokeRoutine(Action f, float delay)
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return null;
            }
            f();
        }

        //Call on a Scroll Rect's content holder to position that holder such that "snap" is visable
        public static void MoveContentToReveal(this RectTransform snap)//, RectTransform scrollRect)
        {
            var sroll = snap.GetComponentInParent<UnityEngine.UI.ScrollRect>();
            RectTransform scrollRect = (RectTransform)sroll.transform;
            RectTransform contentRect = sroll.content;

            if (!scrollRect.Contains(snap))
                contentRect.anchoredPosition =
                        (Vector2)scrollRect.InverseTransformPoint(contentRect.position)
                        - new Vector2(0, scrollRect.InverseTransformPoint(snap.position).y);
        }

        //Links Navigations together (vertical);
        public static Selectable LinkNavigationVertical(Selectable[] fileBtns)
        {
            int len = fileBtns.Length;
            for (int i = 0; i < len; i++)
            {
                Navigation n = fileBtns[i].navigation;
                n.selectOnUp = fileBtns[(i - 1).LoopInt(len)];
                n.selectOnDown = fileBtns[(i + 1).LoopInt(len)];
                fileBtns[i].navigation = n;
            }
            return fileBtns[0];
        }

        //Mimics the function of the same name that exists on Rigidbody
        public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition)//, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
        {
            var explosionDir = rb.position - explosionPosition;
            rb.AddForce(explosionForce * explosionDir);
        }

        //Fade Canvas Coroutine
        public static void Fade(this CanvasGroup cGroup, bool endAlpha, float timeTaken = 0.3f, float delay = 0f)
        {
            Fade(cGroup, endAlpha.ToInt(), timeTaken, delay, endAlpha);
        }
        public static void Fade(this CanvasGroup cGroup, float endAlpha, float timeTaken = 0.3f, float delay = 0f)
        {
            Fade(cGroup, endAlpha, timeTaken, delay, endAlpha > 0);
        }

        public static void Fade(this CanvasGroup cGroup, float endAlpha, float timeTaken = 0.3f, float delay = 0f, bool activeAfter = true)
        {
            if (cGroup.alpha == endAlpha && cGroup.gameObject.activeSelf == activeAfter) //Check if nothing will be changed
                return;
            if (!cGroup.gameObject.activeSelf && !activeAfter) //Check if it is never turning on (just change alpha value without time/delay)
            {
                cGroup.alpha = endAlpha;
                return;
            }
            cGroup.gameObject.SetActive(true); //Needs to be here since you cant start a coroutine on an inactive object
            cGroup.GetComponent<MonoBehaviour>().StartCoroutine(FadeRoutine(cGroup, endAlpha, timeTaken, delay, activeAfter));
        }

        private static IEnumerator FadeRoutine(this CanvasGroup cGroup, float endAlpha, float timeTaken, float delay = 0f, bool activeAfter = true)
        {
            float startAlpha = cGroup.alpha;
            float timer = 0;
            cGroup.interactable = cGroup.blocksRaycasts = false;
            yield return new WaitForSeconds(delay);
            while (timer <= 1)
            {
                cGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timer);
                timer += Time.deltaTime / timeTaken;
                yield return null;
            }
            cGroup.alpha = endAlpha;

            cGroup.interactable = cGroup.blocksRaycasts = activeAfter;
            cGroup.gameObject.SetActive(activeAfter);
        }

        //Get the total force of a 2D impact, simmilar to Collision.impulse - http://answers.unity.com/answers/1906926/view.html
        public static float GetImpactForceSum(this Collision2D collision)
        {
            ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
            collision.GetContacts(contacts);
            float totalImpulse = 0;
            foreach (ContactPoint2D contact in contacts)
            {
                totalImpulse += contact.normalImpulse;
            }
            return totalImpulse;
        }
    }
}