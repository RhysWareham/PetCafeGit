using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private PreCookingUI recipeMenu;
    [SerializeField] private MidCookingUI midCookingMenu;
    [SerializeField] private PostCookingUI postCookingMenu;

    public Machine currentMachine;

    public Machine CurrentMachine => currentMachine;

    void Awake()
    {
        Instance = this;
    }

    public void OpenRecipeMenu(Machine machine)
    {
        CloseAll();

        currentMachine = machine;
        recipeMenu.Open(machine);
    }

    public void OpenMidCookingMenu(Machine machine)
    {
        CloseAll();

        currentMachine = machine;
        midCookingMenu.Open(machine);
    }

    public void OpenPostCookingMenu(Machine machine)
    {
        CloseAll();

        currentMachine = machine;
        postCookingMenu.Open(machine);
    }

    public void CloseAll()
    {
        currentMachine = null;

        recipeMenu.HideMenu();
        midCookingMenu.HideMenu();
        postCookingMenu.HideMenu();
    }
}