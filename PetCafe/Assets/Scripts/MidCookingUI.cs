using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MidCookingUI : MonoBehaviour
{
    public static MidCookingUI Instance { get; private set; }

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

    public void OnCancelPressed()
    {
        //Open "are you sure" menu first
        if (currentMachine != null)
        {
            currentMachine.RemoveFood();
        }

        HideMenu();
    }

    public void OnSkipPressed()
    {
        //Get cooking time leftover and workout price based from that.
        //Open a small are you sure menu
        if (currentMachine != null)
        {
            currentMachine.FinishCookingFood();
        }

        //Change menu to post cooking ui
        UIManager.Instance.OpenPostCookingMenu(currentMachine);
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