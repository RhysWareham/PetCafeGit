using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class PreCookingUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private Machine currentMachine;
    [SerializeField] private RecipeDatabase database;
    [SerializeField] private Transform contentHolder;
    private List<GameObject> menuItems = new List<GameObject>();
    [SerializeField] private GameObject MenuItemGO;
    [SerializeField] private ContentScrollerControl contentScrollerControl;


    void OnEnable()
    {
        //GameEvents.OnMachineSelected += Open;
    }

    void OnDisable()
    {
        //GameEvents.OnMachineSelected -= Open;
    }

    public void Open(Machine machine)
    {
        currentMachine = machine;

        var validRecipes = database.GetRecipesForCategory(machine.machineType);
        int currentLevel = ExperienceManager.Instance.CurrentLevel;

        foreach (Transform child in contentHolder)
            Destroy(child.gameObject);

        for (int i = 0; i < validRecipes.Count; i++)
        {
            var menuItem = Instantiate(MenuItemGO, contentHolder.transform);
            menuItem.GetComponent<MenuItem>().SetupMenuItem(this, validRecipes[i], currentLevel);
            menuItems.Add(menuItem);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(contentHolder as RectTransform);
        contentScrollerControl.UpdateScrollState();

        ShowMenu();
    }

    public void Close()
    {
        UIManager.Instance.CloseAll();

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
