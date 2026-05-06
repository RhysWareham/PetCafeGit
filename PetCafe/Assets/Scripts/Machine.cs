using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{
    public bool isCooking { get; private set; }
    public Categories.MachineType machineType;
    private bool machineOccupied = false;
    private float cookingStartTime = -1;

    private FoodRecipe foodBeingCooked;
    [SerializeField] private float maxWidth = 1.5f;
    [SerializeField] private float maxHeight = 1.0f;
    [SerializeField] private SpriteRenderer foodSprite;

    private Coroutine cookingCoroutine;
    [SerializeField] private CookingProgressUI cookingProgressUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCooking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FoodRecipe GetFood()
    {
        return foodBeingCooked;
    }

    public void Interact()
    {
        GameEvents.OnMachineTapped?.Invoke(this);

        if (isCooking)
        {
            UIManager.Instance.OpenMidCookingMenu(this);
        }
        else if (!machineOccupied)
        {
            UIManager.Instance.OpenRecipeMenu(this);
        }
        else
        {
            UIManager.Instance.OpenPostCookingMenu(this);
        }
    }

    public void StartCooking(FoodRecipe recipe)
    {
        isCooking = true;
        machineOccupied = true;

        cookingCoroutine = StartCoroutine(Cooking(recipe));
        cookingStartTime = Time.time;
    }

    private IEnumerator Cooking(FoodRecipe recipe)
    {
        foodBeingCooked = recipe;
        foodSprite.sprite = recipe.cookingSprite;
        FitSpriteToContainer(foodSprite);

        float elapsed = 0f;
        int cookTime = recipe.cookTime;

        while (elapsed < cookTime)
        {
            elapsed += Time.deltaTime;

            float progress = elapsed / cookTime;

            // Only update if this machine is selected
            if (MidCookingUI.Instance.CurrentMachine == this)
            {
                MidCookingUI.Instance.UpdateProgress(progress, cookTime - elapsed);
            }

            yield return null;
        }

        FinishCookingFood();
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

    public void FinishCookingFood()
    {
        if (cookingCoroutine != null)
        {
            StopCoroutine(cookingCoroutine);
            cookingCoroutine = null;
        }

        // Only update if this machine is selected
        if (UIManager.Instance.CurrentMachine == this)
        {
            UIManager.Instance.OpenPostCookingMenu(this);
        }

        foodSprite.sprite = foodBeingCooked.cookedSprite;
        FitSpriteToContainer(foodSprite);
        cookingStartTime = -1;
        isCooking = false;
    }

    public void OnMoveToCounterComplete()
    {
        ExperienceManager.Instance.AddXP(foodBeingCooked.xpReward);

        RemoveFood();
    }

    public void RemoveFood()
    {
        if (cookingCoroutine != null)
        {
            StopCoroutine(cookingCoroutine);
            cookingCoroutine = null;
        }

        isCooking = false;
        foodSprite.sprite = null;
        machineOccupied = false;
        foodBeingCooked = null;
        cookingStartTime = -1;
    }
}
