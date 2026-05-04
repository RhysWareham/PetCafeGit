using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Animation")]
    [SerializeField] private float animationDuration = 0.5f;
    private float animationTimer;
    private float startFill;
    private bool isAnimating;

    [Header("Level Up Flash")]
    [SerializeField] private Image flashImage; // full bar overlay (white image)
    [SerializeField] private float flashDuration = 0.3f;

    private float displayedFill;
    private float targetFill;

    private int currentLevel;

    private float flashTimer;
    private bool isFlashing;


    void Start()
    {
        var xpManager = ExperienceManager.Instance;

        currentLevel = xpManager.CurrentLevel;

        UpdateUI(xpManager.CurrentXP, GetXPRequired(), currentLevel);

        xpManager.OnXPChanged += OnXPChanged;
    }

    void OnDestroy()
    {
        if (ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.OnXPChanged -= OnXPChanged;
        }
    }

    void OnXPChanged(int currentXP, int xpToNext, int level)
    {
        float newFill = (float)currentXP / xpToNext;

        // 🎉 Level up detected
        if (level > currentLevel)
        {
            currentLevel = level;

            TriggerFlash();

            // reset bar visually for new level
            displayedFill = 0f;
            fillImage.fillAmount = 0f;

            startFill = 0f;
        }
        else
        {
            startFill = displayedFill;
        }

        targetFill = newFill;

        animationTimer = 0f;
        isAnimating = true;

        levelText.text = $"Lv {level}";
    }

    void Update()
    {
        if (isAnimating)
        {
            animationTimer += Time.deltaTime;

            float t = animationTimer / animationDuration;

            if (t >= 1f)
            {
                t = 1f;
                isAnimating = false;
            }

            // ⭐ Ease-out
            t = 1f - Mathf.Pow(1f - t, 3f);

            displayedFill = Mathf.Lerp(startFill, targetFill, t);
            fillImage.fillAmount = displayedFill;
        }


        if (isFlashing)
        {
            flashTimer += Time.deltaTime;

            float t = flashTimer / flashDuration;

            // fade out
            Color c = flashImage.color;
            c.a = 1f - t;
            flashImage.color = c;

            if (t >= 1f)
            {
                isFlashing = false;
                flashImage.gameObject.SetActive(false);
            }
        }
    }

    void UpdateUI(int currentXP, int xpToNext, int level)
    {
        float fill = (float)currentXP / xpToNext;

        displayedFill = fill;
        targetFill = fill;
        startFill = fill;

        fillImage.fillAmount = fill;
        levelText.text = $"Lv {level}";


    }

    void TriggerFlash()
    {
        flashImage.gameObject.SetActive(true);

        flashTimer = 0f;
        isFlashing = true;

        Color c = flashImage.color;
        c.a = 1f;
        flashImage.color = c;

        //Play level up sound
    }


    int GetXPRequired()
    {
        return 100 + (ExperienceManager.Instance.CurrentLevel - 1) * 50;
    }
}