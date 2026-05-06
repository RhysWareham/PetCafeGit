using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MoveManager : MonoBehaviour
{
    public static MoveManager Instance;

    private Machine sourceMachine;
    private List<Counter> allCounters;

    public bool IsMoving => sourceMachine != null;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        allCounters = FindObjectsByType<Counter>(FindObjectsSortMode.None).ToList();
    }

    public void StartMove(Machine machine)
    {
        sourceMachine = machine;

        foreach (var counter in allCounters)
        {
            counter.SetCounterHighlight(true);
        }
    }

    public void CompleteMove(Counter targetCounter)
    {
        if (sourceMachine == null || targetCounter == null)
            return;

        if (targetCounter.IsOccupied)
            return;

        targetCounter.PlaceFood(sourceMachine.GetFood());

        sourceMachine.OnMoveToCounterComplete();

        ExitMoveMode();
    }

    public void ExitMoveMode()
    {
        foreach (var counter in allCounters)
        {
            counter.SetCounterHighlight(false);
        }

        sourceMachine = null;
    }
}