using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System.Collections;

public class PreCookingUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform panel;
    [SerializeField] private float animTime = 0.2f;
    private Machine currentMachine;
    [SerializeField] private RecipeDatabase database;
    [SerializeField] private Transform contentHolder;
    private List<GameObject> menuItems = new List<GameObject>();
    [SerializeField] private GameObject MenuItemGO;
    [SerializeField] private ContentScrollerControl contentScrollerControl;
    private bool menuIsOpen = false;


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
        StartCoroutine(OpenAnim());

        //canvasGroup.alpha = 1;
        //canvasGroup.interactable = true;
        //canvasGroup.blocksRaycasts = true;
    }

    IEnumerator OpenAnim()
    {
        menuIsOpen = true;

        canvasGroup.alpha = 0;
        panel.localScale = Vector3.one * 0.9f;

        float t = 0;

        while (t < animTime)
        {
            t += Time.deltaTime;
            float k = t / animTime;

            canvasGroup.alpha = k;
            panel.localScale = Vector3.Lerp(Vector3.one * 0.9f, Vector3.one, k);

            yield return null;
        }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        panel.localScale = Vector3.one;
    }

    public void HideMenu()
    {
        if (menuIsOpen)
        {
            StartCoroutine(CloseAnim());
        }

        //canvasGroup.alpha = 0;
        //canvasGroup.interactable = false;
        //canvasGroup.blocksRaycasts = false;
    }

    IEnumerator CloseAnim()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        menuIsOpen = false;

        float t = 0;

        Vector3 startScale = panel.localScale;

        while (t < animTime)
        {
            t += Time.deltaTime;
            float k = t / animTime;

            canvasGroup.alpha = 1 - k;
            panel.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.9f, k);

            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}
