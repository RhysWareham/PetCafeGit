using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CookingProgressUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI timeText;

    public void SetProgress(float t, float timeLeft)
    {
        fillImage.fillAmount = t;

        timeText.text = UIUtility.WorkOutTimeInHMS(timeLeft);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}