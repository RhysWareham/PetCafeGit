using UnityEngine;
using TMPro;
using System.Collections;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float floatHeight = 0.5f;

    private WorldToUIFollow follow;

    void Awake()
    {
        follow = GetComponent<WorldToUIFollow>();
    }

    public void Setup(string value)
    {
        text.text = value;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float time = 0f;

        Vector3 startOffset = follow.Offset;
        Vector3 endOffset = startOffset + Vector3.up * floatHeight;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // Ease-out (nice feel)
            float eased = 1 - Mathf.Pow(1 - t, 3);

            follow.Offset = Vector3.Lerp(startOffset, endOffset, eased);

            canvasGroup.alpha = 1 - t;

            yield return null;
        }

        Destroy(gameObject);
    }
}