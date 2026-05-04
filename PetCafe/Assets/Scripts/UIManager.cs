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
        currentMachine = machine;

        CloseAll();

        recipeMenu.Open(machine);
    }

    public void OpenMidCookingMenu(Machine machine)
    {
        currentMachine = machine;

        CloseAll();

        midCookingMenu.Open(machine);
    }

    public void OpenPostCookingMenu(Machine machine)
    {
        currentMachine = machine;

        CloseAll();

        postCookingMenu.Open(machine);
    }

    public void CloseAll()
    {
        recipeMenu.HideMenu();
        midCookingMenu.HideMenu();
        postCookingMenu.HideMenu();
    }
}