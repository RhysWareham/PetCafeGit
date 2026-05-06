using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class XPBarUI : MonoBehaviour
{
    [SerializeField] private RecipeDatabase recipeDatabase;

    [Header("UI")]
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Animation")]
    [SerializeField] private float animationDuration = 0.4f;
    [SerializeField] private float immediateFillSpeedMultiplier = 0.6f;
    [SerializeField] private float easePower = 2f;

    [Header("Level Up Flash")]
    [SerializeField] private Image flashImage;
    [SerializeField] private float flashDuration = 0.3f;

    private int levelBeforeXP;
    private int currentLevel;
    private int pendingLevel;
    private int pendingXP;
    private int pendingXPToNext;


    private Coroutine animationRoutine;

    void Start()
    {
        var xp = ExperienceManager.Instance;

        currentLevel = xp.CurrentLevel;

        float fill = (float)xp.CurrentXP / GetXPRequired();
        fillImage.fillAmount = fill;

        levelText.text = $"Lv {currentLevel}";

        xp.OnXPChanged += OnXPChanged;
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
        pendingLevel = level;
        pendingXP = currentXP;
        pendingXPToNext = xpToNext;

        if (animationRoutine == null)
        {
            levelBeforeXP = currentLevel;
            animationRoutine = StartCoroutine(AnimateXP());
        }
    }

    IEnumerator AnimateXP()
    {
        while (currentLevel < pendingLevel)
        {
            yield return AnimateFill(fillImage.fillAmount, 1f, false);


            StartCoroutine(LevelUpStep(++currentLevel));

            fillImage.fillAmount = 0f;
        }

        // Final partial fill
        float finalFill = (float)pendingXP / pendingXPToNext;

        if (Mathf.Abs(fillImage.fillAmount - finalFill) > 0.001f)
        {
            yield return AnimateFill(fillImage.fillAmount, finalFill, true);
        }

        UnlockManager.Instance.HandleLevelUps(levelBeforeXP, pendingLevel);

        animationRoutine = null;
    }

    IEnumerator AnimateFill(float start, float end, bool useEase)
    {
        float timer = 0f;
        float duration = useEase ? animationDuration : animationDuration * immediateFillSpeedMultiplier;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            if (useEase)
            {
                t = 1f - Mathf.Pow(1f - t, easePower);
            }

            float value = Mathf.Lerp(start, end, t);
            fillImage.fillAmount = value;

            yield return null;
        }

        fillImage.fillAmount = end;
    }


    IEnumerator LevelUpStep(int newLevel)
    {
        levelText.text = $"Lv {newLevel}";

        flashImage.gameObject.SetActive(true);

        float timer = 0f;

        while (timer < flashDuration)
        {
            timer += Time.deltaTime;

            float t = timer / flashDuration;

            Color c = flashImage.color;
            c.a = 1f - t;
            flashImage.color = c;

            yield return null;
        }

        flashImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.05f);
    }

    int GetXPRequired()
    {
        return 100 + (ExperienceManager.Instance.CurrentLevel - 1) * 50;
    }
}
