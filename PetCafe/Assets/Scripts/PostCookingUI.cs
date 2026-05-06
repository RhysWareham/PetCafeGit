using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PostCookingUI : MonoBehaviour
{
    public static PostCookingUI Instance { get; private set; }

    [SerializeField] private CanvasGroup canvasGroup;
    private WorldToUIFollow uiFollowScript;
    public Machine currentMachine;

    public Machine CurrentMachine => currentMachine;

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

    public void Open(Machine machine)
    {
        currentMachine = machine;

        uiFollowScript.SetTarget(machine.gameObject.transform);

        ShowMenu();
    }

    public void OnDeletePressed()
    {
        //Open "are you sure" menu first
        if (currentMachine != null)
        {
            currentMachine.RemoveFood();
        }

        UIManager.Instance.CloseAll();
    }

    public void OnMovePressed()
    {
        //Select a counter to move the food to
        if (currentMachine != null)
        {
            currentMachine.OnMoveToCounter();
            Debug.Log("Food has been moved to table");
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

        currentMachine = null;
        uiFollowScript.ClearTarget();
    }
}