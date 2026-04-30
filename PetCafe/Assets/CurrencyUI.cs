using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("Count animation")]
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float popScale = 1.2f;
    [SerializeField] private float popSpeed = 8f;

    private float displayedAmount;
    private float targetAmount;

    private Vector3 originalScale;

    void OnEnable()
    {
        CurrencyManager.Instance.OnMoneyChanged += OnMoneyChanged;

        displayedAmount = CurrencyManager.Instance.CurrentMoney;
        targetAmount = displayedAmount;

        originalScale = transform.localScale;

        UpdateText((int)displayedAmount);
    }

    void OnDisable()
    {
        CurrencyManager.Instance.OnMoneyChanged -= OnMoneyChanged;
    }

    void OnMoneyChanged(int newAmount)
    {
        targetAmount = newAmount;

        transform.localScale = originalScale * popScale;
    }

    void Update()
    {
        if (!Mathf.Approximately(displayedAmount, targetAmount))
        {
            float difference = targetAmount - displayedAmount;

            float speed = Mathf.Abs(difference) / animationDuration;
            float step = speed * Time.deltaTime;

            step = Mathf.Max(step, 1f);

            displayedAmount = Mathf.MoveTowards(displayedAmount, targetAmount, step);

            if (Mathf.Abs(displayedAmount - targetAmount) < 0.5f)
            {
                displayedAmount = targetAmount;
            }

            UpdateText(Mathf.RoundToInt(displayedAmount));
        }

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            originalScale,
            popSpeed * Time.deltaTime
        );
    }

    void UpdateText(int amount)
    {
        moneyText.text = $"£{amount}";
    }
}