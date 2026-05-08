using System.Collections;
using UnityEngine;

public class Counter : Workstation
{
    public bool counterOccupied { get; private set; }
    private int mealsRemaining = 0;
    private bool isDirty;

    private FoodRecipe foodOnCounter;
    [SerializeField] private int cleanCounterXP = 10;
    [SerializeField] private float maxWidth = 1.5f;
    [SerializeField] private float maxHeight = 1.0f;
    [SerializeField] private SpriteRenderer foodSprite;
    [SerializeField] private SpriteRenderer counterSprite;
    [SerializeField] private GameObject cleanHighlight;
    [SerializeField] private GameObject dirtyHighlight;
    private bool isHighlighting = false;

    [SerializeField] private Transform customerSalePoint;
    [SerializeField] private bool isReserved;

    private Coroutine sellingCoroutine;

    public bool IsOccupied => counterOccupied;
    public bool IsDirty => isDirty;
    public int MealsRemaining => mealsRemaining;
    public bool IsReserved => isReserved;
    public Transform CustomerSalePoint => customerSalePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCounterHighlight(bool turnOn)
    {
        if (!counterOccupied)
        {
            isHighlighting = turnOn;

            if (isDirty)
            {
                dirtyHighlight.SetActive(turnOn);
            }
            else
            {
                cleanHighlight.SetActive(turnOn);
            }
        }
        else if (!turnOn)
        {
            isHighlighting = turnOn;

            dirtyHighlight.SetActive(turnOn);
            cleanHighlight.SetActive(turnOn);
        }
    }

    public void Interact()
    {
        if (isDirty)
        {
            CleanCounter();
            return;
        }

        if (MoveManager.Instance.IsMoving)
        {
            MoveManager.Instance.CompleteMove(this);
            return;
        }

        if (!counterOccupied)
            return;

        UIManager.Instance.OpenCounterMenu(this);
    }

    public void PlaceFood(FoodRecipe recipe)
    {
        foodOnCounter = recipe;
        counterOccupied = true;

        mealsRemaining = recipe.maxMeals;
        isDirty = false;

        foodSprite.sprite = recipe.cookedSprite;
        FitSpriteToContainer(foodSprite);
        cleanHighlight.SetActive(false);
    }

    public void SellMeal()
    {
        if (mealsRemaining <= 0)
            return;

        mealsRemaining--;

        CurrencyManager.Instance.AddMoney(foodOnCounter.profitPerMeal);

        SaleValueManager.Instance.SpawnFloatingText(foodOnCounter.profitPerMeal, this);

        if (mealsRemaining <= 0)
        {
            RemoveFood();
        }
    }

    public void BecomeDirty()
    {
        isDirty = true;

        SetDirtyVisual(true);
    }

    public void CleanCounter()
    {
        isDirty = false;

        if (isHighlighting)
        {
            dirtyHighlight.SetActive(false);
            cleanHighlight.SetActive(true);
        }
        else
        {
            dirtyHighlight.SetActive(false);
        }

        SetDirtyVisual(false);

        ExperienceManager.Instance.AddXP(cleanCounterXP);
        XPValueManager.Instance.SpawnFloatingText(cleanCounterXP, this);

    }

    private void SetDirtyVisual(bool dirty)
    {
        if (dirty)
        {
            counterSprite.color = new Color(1f, 0.5f, 0f); //Orange
        }
        else
        {
            counterSprite.color = Color.white;
        }
    }

    void FitSpriteToContainer(SpriteRenderer sr)
    {
        if (sr.sprite == null) return;

        //Bounds counterBounds = counterSprite.bounds;

        //float maxWidth = counterBounds.size.x * 0.8f;
        //float maxHeight = counterBounds.size.y * 0.5f;

        Vector2 spriteSize = sr.sprite.bounds.size;

        float scaleX = maxWidth / spriteSize.x;
        float scaleY = maxHeight / spriteSize.y;

        float scale = Mathf.Min(scaleX, scaleY);

        sr.transform.localScale = new Vector3(scale, scale, 1f);
    }

    public void RemoveFood()
    {
        if (sellingCoroutine != null)
        {
            StopCoroutine(sellingCoroutine);
            sellingCoroutine = null;
        }

        BecomeDirty();

        foodSprite.sprite = null;
        counterOccupied = false;
        foodOnCounter = null;
        mealsRemaining = 0;
        isReserved = false;
    }

    public void Reserve()
    {
        isReserved = true;
    }

    public void Unreserve()
    {
        isReserved = false;
    }

    public bool HasFoodAvailable =>
        counterOccupied &&
        mealsRemaining > 0 &&
        !isReserved;
}
