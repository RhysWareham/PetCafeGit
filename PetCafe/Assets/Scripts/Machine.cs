using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{
    private bool isCooking = false;
    public Categories.CookingCategory categoryCanCook;
    private bool machineOccupied = false;
    private float cookingStartTime;

    private FoodRecipe foodBeingCooked;
    [SerializeField] private SpriteRenderer foodSprite;


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
        if (!isCooking)
        {
            GameEvents.OnMachineSelected?.Invoke(this);
        }
    }

    public void StartCooking(FoodRecipe recipe)
    {
        isCooking = true;
        machineOccupied = true;

        StartCoroutine(Cooking(recipe));
        cookingStartTime = Time.time;
    }

    private IEnumerator Cooking(FoodRecipe recipe)
    {
        foodSprite.sprite = recipe.cookingSprite;

        yield return new WaitForSecondsRealtime(recipe.cookTime);

        foodSprite.sprite = recipe.cookedSprite;
        isCooking = false;
    }

    private void RemoveFood()
    {
        isCooking = false;
        foodSprite.sprite = null;
        machineOccupied = false;
    }
}
