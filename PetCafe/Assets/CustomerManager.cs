using System.Collections.Generic;
using UnityEngine;
using static Customer;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;

    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform customerSpawnPoint;
    [SerializeField] private Transform[] queuePositions;
    private List<Customer> customers = new List<Customer>();

    [SerializeField] private float minFrequencyToSpawn;
    [SerializeField] private float maxFrequencyToSpawn;
    private float nextSpawnTime;

    private int numOfCustomersInQueue = 0;
    private int maxNumOfCustomersInQueue = 5;

    public Transform[] QueuePositions => queuePositions;

    private int allCustomers = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            if (numOfCustomersInQueue < maxNumOfCustomersInQueue)
            { 
                SpawnCustomer();
                nextSpawnTime = Time.time + Random.Range( minFrequencyToSpawn, maxFrequencyToSpawn );
            }
        }
    }

    private void SpawnCustomer()
    {
        GameObject go = Instantiate(customerPrefab, customerSpawnPoint.position, Quaternion.identity);
        go.name = "customer" + allCustomers.ToString();
        Customer customer = go.GetComponent<Customer>();

        customers.Add(customer);

        RefreshQueue();

        customer.Setup();

        numOfCustomersInQueue++;
        allCustomers++;
    }

    public int GetQueueIndex(Customer customer)
    {
        int index = 0;

        foreach (var c in customers)
        {
            if (c == customer)
                break;

            if (c.CurrentState == CustomerState.WaitingInQueue ||
                c.CurrentState == CustomerState.WalkingToQueue)
            {
                index++;
            }
        }

        return index;
    }

    public bool IsFrontOfQueue(Customer customer)
    {
        foreach (var c in customers)
        {
            if (c.CurrentState == CustomerState.WaitingInQueue ||
                c.CurrentState == CustomerState.WalkingToQueue)
            {
                
                return c == customer && c.HasReachedQueuePosition;
            }
        }

        return false;
    }

    public void CustomerLeft(Customer customer)
    {
        numOfCustomersInQueue--;
        customers.Remove(customer);

        RefreshQueue();
    }

    public void RefreshQueue()
    {
        foreach (var customer in customers)
        {
            customer.UpdateQueuePosition();
        }
    }
}
