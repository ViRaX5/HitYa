using UnityEngine;

// Put this on a full-screen Panel inside your Canvas.
// Everything inside the Panel stays inside the safe area.
[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    Rect lastSafeArea;

    void Update()
    {
        Rect safe = Screen.safeArea;
        if (safe == lastSafeArea) return;          // only when it changes
        lastSafeArea = safe;

        var panel = GetComponent<RectTransform>();
        panel.anchorMin = new Vector2(safe.xMin / Screen.width, safe.yMin / Screen.height);
        panel.anchorMax = new Vector2(safe.xMax / Screen.width, safe.yMax / Screen.height);
        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;
    }
}

