using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{
    private bool isCooking = false;
    public Categories.CookingCategory categoryCanCook;
    private bool machineOccupied = false;
    private float cookingStartTime = -1;

    private FoodRecipe foodBeingCooked;
    [SerializeField] private float maxWidth = 1.5f;
    [SerializeField] private float maxHeight = 1.0f;
    [SerializeField] private SpriteRenderer foodSprite;

    private Coroutine cookingCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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

        yield return new WaitForSecondsRealtime(recipe.cookTime);

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

        foodSprite.sprite = foodBeingCooked.cookedSprite;
        FitSpriteToContainer(foodSprite);
        cookingStartTime = -1;
        isCooking = false;
    }

    public void RemoveFood()
    {
        isCooking = false;
        foodSprite.sprite = null;
        machineOccupied = false;
        foodBeingCooked = null;
        cookingStartTime = -1;
    }
}
