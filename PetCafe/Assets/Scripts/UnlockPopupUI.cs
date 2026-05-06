using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnlockPopupUI : MonoBehaviour
{
    public static UnlockPopupUI Instance;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI levelUnlockedTitle;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform contentHolder;
    [SerializeField] private ContentScrollerControl contentScrollerControl;


    void Awake()
    {
        Instance = this;
    }

    public void Show(List<FoodRecipe> recipes, int levelBefore, int levelAfter)
    {
        levelUnlockedTitle.text = $"LEVEL UP! {levelBefore} -> {levelAfter} UNLOCKED:";

        foreach (Transform child in contentHolder)
            Destroy(child.gameObject);

        foreach (var recipe in recipes)
        {
            var go = Instantiate(itemPrefab, contentHolder.transform);
            go.GetComponent<UnlockedItemUI>().Setup(recipe);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(contentHolder as RectTransform);
        contentScrollerControl.UpdateScrollState();

        ShowMenu();
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
