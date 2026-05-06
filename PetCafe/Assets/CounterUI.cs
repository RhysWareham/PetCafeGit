using UnityEngine;
using TMPro;

public class CounterUI : MonoBehaviour
{
    public static CounterUI Instance { get; private set; }

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI mealsRemainingText;
    private WorldToUIFollow uiFollowScript;

    public Counter currentCounter;

    public Counter CurrentCounter => currentCounter;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        uiFollowScript = this.GetComponent<WorldToUIFollow>();
    }

    private void Update()
    {
        if (currentCounter != null && currentCounter.IsOccupied)
        {
            int mealsRemaining = currentCounter.MealsRemaining;
            if (mealsRemaining > 0)
            {
                mealsRemainingText.text = mealsRemaining.ToString();
            }
            else
            {
                mealsRemainingText.text = "";
            }
        }
        else
        {
            mealsRemainingText.text = "";
        }
    }

    public void Open(Counter counter)
    {
        currentCounter = counter;

        uiFollowScript.SetTarget(counter.gameObject.transform);

        ShowMenu();
    }

    public void OnDeletePressed()
    {
        //Open "are you sure" menu first
        if (currentCounter != null)
        {
            currentCounter.RemoveFood();
        }

        UIManager.Instance.CloseAll();
    }

    public void ShowMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HideMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        currentCounter = null;
        uiFollowScript.ClearTarget();
    }
}
