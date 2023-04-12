using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.General
{
    public static class CustomObjects 
    {
        private const int HEADER_HEIGHT = 40;

        private static RectTransform TopLeftRectTransform(GameObject parent)
        {
            RectTransform rt = parent.AddComponent(typeof(RectTransform)) as RectTransform;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.pivot = new Vector2(0, 1);

            return rt;
        }

        private static GameObject FilledPanel(
            GameObject parent,
            string name,
            float right,
            float bottom,
            Color backgroundColor,
            float top = 0,
            float left = 0,
            string backgroundImageResourceLocation = null,
            string text = null,
            string fontName = "COUR SDF",
            bool noFill = false
        )
        {
            GameObject panelGO = new GameObject(name);
            panelGO.transform.parent = parent.transform;

            CanvasRenderer panelCR = (CanvasRenderer)panelGO.AddComponent(typeof(CanvasRenderer));
            panelCR.cullTransparentMesh = true;

            RectTransform panelRT = TopLeftRectTransform(panelGO);
            panelRT.offsetMin = new Vector2(left, bottom);
            panelRT.offsetMax = new Vector2(-right, -top);

            if (!noFill)
            {
                Image panelImg = (Image)panelGO.AddComponent(typeof(Image));
                panelImg.color = backgroundColor;
                if (!string.IsNullOrEmpty(backgroundImageResourceLocation))
                {
                    panelImg.sprite = Resources.Load<Sprite>(backgroundImageResourceLocation);
                    panelImg.type = Image.Type.Sliced;
                }
            }

            // Title Text
            if (!string.IsNullOrEmpty(text))
            {
                GameObject titleGO = new GameObject();
                titleGO.transform.parent = panelGO.transform;
                titleGO.name = "Title";

                CanvasRenderer titleCR = titleGO.AddComponent(typeof(CanvasRenderer)) as CanvasRenderer;
                titleCR.cullTransparentMesh = true;

                RectTransform titleRT = TopLeftRectTransform(titleGO);
                titleRT.offsetMin = Vector2.zero;
                titleRT.offsetMax = Vector2.zero;

                TextMeshProUGUI titleTMP = titleGO.AddComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
                titleTMP.fontSize = 24;
                titleTMP.text = text;
                titleTMP.color = new Color(1, 1, 1, 1);
                titleTMP.font = Resources.Load<TMP_FontAsset>("Fonts/" + fontName);
                titleTMP.isOrthographic = true;
                titleTMP.horizontalAlignment = HorizontalAlignmentOptions.Center;
                titleTMP.verticalAlignment = VerticalAlignmentOptions.Middle;
            }


            return panelGO;
        }

        public static TextMeshProUGUI CreatePanel(GameObject canvas, string name, string title, float x, float y, float width, float height)
        {
            RectTransform canvasRT = canvas.GetComponent("RectTransform") as RectTransform;
            float canvasWidth = canvasRT.rect.width;
            float canvasHeight = canvasRT.rect.height;

            // Top Level Panel
            float panelRight = canvasWidth - (width + x);
            float panelBottom = canvasHeight - height - y;

            GameObject outerPanel = FilledPanel(canvas, name, panelRight, panelBottom, Color.white, left: x, top: y);

            // Header Panel
            GameObject headerPanel = FilledPanel(outerPanel, "Header", 2, height - (HEADER_HEIGHT + 4), new Color(0, 0.25f, 0, 255), text: title, fontName: "COUR BD SDF", left: 2, top: 2);

            // Background for scrollable area
            GameObject scrollAreaGO = FilledPanel(outerPanel, "Scroller", 2, 2, Color.black, left: 2, top: HEADER_HEIGHT + 6);

            ScrollRect scrollAreaSR = scrollAreaGO.AddComponent(typeof(ScrollRect)) as ScrollRect;

            // Viewport for scrollable area
            GameObject viewportGO = FilledPanel(scrollAreaGO, "Viewport", 0, 0, Color.black);

            Mask viewportMask = viewportGO.AddComponent(typeof(Mask)) as Mask;
            viewportMask.enabled = true;

            // Content Area
            GameObject contentGO = FilledPanel(viewportGO, "Content", 0, 0, Color.black, noFill: true);

            TextMeshProUGUI contentTMP = contentGO.AddComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
            contentTMP.fontSize = 18;
            contentTMP.text = "";  //"This\nis\nsome\ntext\na\nb\nc\nd\ne\nf\ng\nh\ni\nj\nk\nl\nm\nn\no\np\nq\nr\ns\nt\nu\nv\nw\nx\ny\nz\n0\n1\n2\n3\n4\n5\n6\n7\n8\n9\n0\n0\n1\n2\n3\n4\n5\n6\n7\n8\n9\n0\na\nb\nc";
            contentTMP.color = new Color(1, 1, 1, 1);
            contentTMP.font = Resources.Load<TMP_FontAsset>("Fonts/COUR SDF");
            contentTMP.isOrthographic = true;
            contentTMP.horizontalAlignment = HorizontalAlignmentOptions.Left;
            contentTMP.verticalAlignment = VerticalAlignmentOptions.Top;
            contentTMP.margin = new Vector4(3, 3, 3, 3);

            ContentSizeFitter contentCSF = contentGO.AddComponent(typeof(ContentSizeFitter)) as ContentSizeFitter;
            contentCSF.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            contentCSF.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            // Vertical Scrollbar Background
            GameObject vScrollGO = new GameObject("ScrollbarVertical");
            vScrollGO.transform.parent = scrollAreaGO.transform;

            RectTransform vScrollRT = TopLeftRectTransform(vScrollGO);
            vScrollRT.offsetMin = new Vector2(panelRight - 22, 2);
            vScrollRT.offsetMax = new Vector2(-2, -2);

            Image vScrollGOImg = (Image)vScrollGO.AddComponent(typeof(Image));
            vScrollGOImg.color = new Color(0.5f, 0.5f, 0.5f, 1);
            vScrollGOImg.sprite = Resources.Load<Sprite>("Graphics/UI/RoundedBorder");
            vScrollGOImg.type = Image.Type.Sliced;

            Scrollbar vScrollBar = vScrollGO.AddComponent(typeof(Scrollbar)) as Scrollbar;
            vScrollBar.direction = Scrollbar.Direction.BottomToTop;

            // Vertical Scrollbar Sliding Area
            GameObject vSlidingGO = new GameObject("SlidingArea");
            vSlidingGO.transform.parent = vScrollGO.transform;

            RectTransform vSlidingRT = TopLeftRectTransform(vSlidingGO);
            vSlidingRT.pivot = new Vector2(0.5f, 0.5f);
            vSlidingRT.offsetMin = new Vector2(10, 10);
            vSlidingRT.offsetMax = new Vector2(-10, -10);

            // Vertical Scrollbar Handle
            GameObject vHandleGO = new GameObject("Handle");
            vHandleGO.transform.parent = vSlidingGO.transform;

            CanvasRenderer vHandleCR = vHandleGO.AddComponent(typeof(CanvasRenderer)) as CanvasRenderer;
            vHandleCR.cullTransparentMesh = true;

            RectTransform vHandleRT = TopLeftRectTransform(vHandleGO);
            vHandleRT.pivot = new Vector2(0.5f, 0.5f);
            vHandleRT.offsetMin = new Vector2(-10, -10);
            vHandleRT.offsetMax = new Vector2(10, 10);

            Image vHandleImg = vHandleGO.AddComponent(typeof(Image)) as Image;
            vHandleImg.sprite = Resources.Load<Sprite>("Graphics/UI/RoundedBorder");
            vHandleImg.color = new Color(0.75f, 0.75f, 0.75f, 1);
            vHandleImg.type = Image.Type.Sliced;
            vScrollBar.handleRect = vHandleRT;

            // Set up scroll area
            scrollAreaSR.horizontal = false;
            scrollAreaSR.vertical = true;
            scrollAreaSR.viewport = viewportGO.GetComponent("RectTransform") as RectTransform;
            scrollAreaSR.content = contentGO.GetComponent("RectTransform") as RectTransform;
            scrollAreaSR.verticalScrollbar = vScrollBar;
            scrollAreaSR.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;

            return contentTMP;
        }
    }
}