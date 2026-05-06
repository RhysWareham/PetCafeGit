using UnityEngine;
using UnityEngine.UI;

public class ContentScrollerControl : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform viewport;
    [SerializeField] private bool isHorizontal;

    public void UpdateScrollState()
    {
        if (isHorizontal)
        {
            float contentSize = content.rect.width;
            float viewportSize = viewport.rect.width;

            bool shouldScroll = contentSize > viewportSize;
            scrollRect.horizontal = shouldScroll;
        }
        else
        {
            float contentSize = content.rect.height;
            float viewportSize = viewport.rect.height;

            bool shouldScroll = contentSize > viewportSize;
            scrollRect.vertical = shouldScroll;
        }
    }
}
