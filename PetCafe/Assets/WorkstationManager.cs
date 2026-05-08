using System.Collections.Generic;
using UnityEngine;

public class WorkstationManager : MonoBehaviour
{
    public static WorkstationManager Instance;

    [SerializeField] private List<Workstation> workstations = new List<Workstation>();

    private void Awake()
    {
        Instance = this;
    }

    public List<Counter> GetCountersWithFood()
    {
        var countersWithFood = new List<Counter>();

        foreach (var workstation in workstations)
        {
            Counter counter = workstation as Counter;

            if (counter != null && counter.HasFoodAvailable)
            {
                countersWithFood.Add(counter);
            }
        }

        return countersWithFood;
    }

    public Counter GetAndReserveCounterWithFood()
    {
        List<Counter> availableCounters = new List<Counter>();

        foreach (var workstation in workstations)
        {
            Counter counter = workstation as Counter;

            if (counter != null && counter.HasFoodAvailable)
            {
                availableCounters.Add(counter);
            }
        }

        if (availableCounters.Count == 0)
            return null;

        Counter chosen =
            availableCounters[Random.Range(0, availableCounters.Count)];

        chosen.Reserve();

        return chosen;
    }
}
