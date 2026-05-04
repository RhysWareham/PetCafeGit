using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CookingProgressUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI timeText;

    public void SetProgress(float progress, float timeLeft)
    {
        fillImage.fillAmount = progress;

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