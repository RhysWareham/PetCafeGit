using UnityEngine;
using UnityEngine.EventSystems;

public static class UIUtility
{
    public static bool IsPointerOverUIWithTag(string tag)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag(tag))
                return true;
        }

        return false;
    }

    public static string WorkOutTimeInHMS(float rawTime)
    {
        int roundedTimeLeft = Mathf.RoundToInt(rawTime);

        int h = roundedTimeLeft / 3600;
        int m = (roundedTimeLeft % 3600) / 60;
        int s = roundedTimeLeft % 60;

        string result = "";

        if (h > 0)
        {
            result = $"{h}h ";
        }

        if (m > 0)
        {
            result += $"{m}m "; 
        }

        if (s > 0 && h == 0)
        {
            result += $"{s}s ";
        }

        return result.TrimEnd();
    }
}