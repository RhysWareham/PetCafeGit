using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CookingUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private Machine currentMachine;
    [SerializeField] private RecipeDatabase database;
    [SerializeField] private Transform recipeStartPosition;
    private List<GameObject> menuItems = new List<GameObject>();
    [SerializeField] private GameObject MenuItemGO;

    void OnEnable()
    {
        GameEvents.OnMachineSelected += Open;
    }

    void OnDisable()
    {
        GameEvents.OnMachineSelected -= Open;
    }

    public void Open(Machine machine)
    {
        currentMachine = machine;

        var validRecipes = database.allRecipes.Where(recipe => recipe.category == machine.categoryCanCook).ToList();

        for (int i = 0; i < validRecipes.Count; i++)
        {
            var menuItem = Instantiate(MenuItemGO, transform);
            menuItem.GetComponent<MenuItem>().SetupMenuItem(this, validRecipes[i], recipeStartPosition, i);
            menuItems.Add(menuItem);
        }

        ShowMenu();
    }

    public void Close()
    {
        HideMenu();

        foreach (GameObject obj in menuItems)
        {
            if (obj != null)
            {
                Object.Destroy(obj);
            }
        }

        menuItems.Clear();
    }

    public void SelectItem(FoodRecipe recipe)
    {
        currentMachine.StartCooking(recipe);

        Close();
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
    }
}
