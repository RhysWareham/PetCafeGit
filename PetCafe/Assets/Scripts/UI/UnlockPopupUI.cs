using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UnlockPopupUI : MonoBehaviour
{
    public static UnlockPopupUI Instance;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform panel;
    [SerializeField] private float animTime = 0.2f;
    [SerializeField] private TextMeshProUGUI levelUnlockedTitle;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform contentHolder;
    [SerializeField] private ContentScrollerControl contentScrollerControl;
    private bool menuIsOpen = false;


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
