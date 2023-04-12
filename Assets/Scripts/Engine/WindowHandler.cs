using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AdventureEngine.Engine
{
    public static class WindowHandler
    {
        private static Dictionary<string, TextMeshProUGUI> windowTMPObjects = new Dictionary<string, TextMeshProUGUI>();

        public static void RegisterWindow(string windowName, TextMeshProUGUI windowTMP)
        {
            windowTMPObjects.Add(windowName, windowTMP);
        }

        public static void SetWindowText(string windowName, string text)
        {
            if (windowTMPObjects.ContainsKey(windowName))
            {
                windowTMPObjects[windowName].text = text;
            }
        }

        public static void AppendWindowText(string windowName, string text)
        {
            if (windowTMPObjects.ContainsKey(windowName))
            {
                windowTMPObjects[windowName].text += text;
            }
        }

    }
}