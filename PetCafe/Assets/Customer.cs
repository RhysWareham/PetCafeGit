using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public enum CustomerState
    {
        WalkingToQueue,
        WaitingInQueue,
        WalkingToCounter,
        Leaving
    }

    [SerializeField] private float moveSpeed = 2f;

    private bool leaving = false;

    [SerializeField] private CustomerState currentState = CustomerState.WalkingToQueue;
    public CustomerState CurrentState => currentState;
    [SerializeField] private bool hasReachedQueuePosition = false;
    public bool HasReachedQueuePosition => hasReachedQueuePosition;

    [Header("Debug")]
    [SerializeField] private Counter chosenCounter;
    [SerializeField] private bool isCounterFree;
    [SerializeField] private bool isHeadingToCounter = false;
    [SerializeField] private bool hasReachedCounter = false;
    [SerializeField] private bool isLeaving = false;

    private Coroutine moveCoroutine;
    private int lockedQueueIndex;
    private int lastQueueIndex = -1;

    public void Setup()
    {
        StartCoroutine(CustomerRoutine());
    }

    IEnumerator CustomerRoutine()
    {
        if (currentState != CustomerState.WalkingToCounter && currentState != CustomerState.Leaving)
        {
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveToQueueRoutine());
        }

        while (true)
        {
            // Only front queued customer can buy
            if (CustomerManager.Instance.IsFrontOfQueue(this))
            {
                Counter counter = WorkstationManager.Instance.GetAndReserveCounterWithFood();

                if (counter != null)
                {
                    chosenCounter = counter;
                    isCounterFree = counter.IsReserved;

                    if (moveCoroutine != null)
                    {
                        StopCoroutine(moveCoroutine);
                        moveCoroutine = null;
                    }

                    moveCoroutine = StartCoroutine(WalkToCounter(counter));

                    yield return moveCoroutine;

                    yield break;
                }
            }


            yield return null;
        }
    }

    IEnumerator WalkToCounter(Counter counter)
    {
        currentState = CustomerState.WalkingToCounter;

        CustomerManager.Instance.RefreshQueue();

        isHeadingToCounter = true;

        yield return MoveTo(counter.CustomerSalePoint.position, true);

        if (currentState != CustomerState.WalkingToCounter)
        {
            Debug.Log("Stopped walking to counter????");
            yield break;
        }

        hasReachedCounter = true;

        counter.SellMeal();

        yield return new WaitForSeconds(0.5f);

        StartLeaving();

        isLeaving = true;

        yield return new WaitForSeconds(0.2f);

        counter.Unreserve();
    }

    IEnumerator MoveTo(Vector3 target, bool toCounter)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            if (chosenCounter != null)
            {
                int x = 0;
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );

            if (target == null)
            {
                Debug.Log("Target has become null");
            }

            yield return null;
        }
    }

    void StartLeaving()
    {
        if (leaving)
            return;

        leaving = true;

        CustomerManager.Instance.CustomerLeft(this);

        StartCoroutine(LeaveRoutine());
    }

    IEnumerator LeaveRoutine()
    {
        Vector3 offscreen = transform.position + Vector3.left * 20f;

        yield return MoveTo(offscreen, false);

        Destroy(gameObject);
    }

    public void UpdateQueuePosition()
    {
        if (currentState == CustomerState.WalkingToCounter ||
            currentState == CustomerState.Leaving)
            return;

        int newIndex = CustomerManager.Instance.GetQueueIndex(this);

        if (newIndex == lastQueueIndex && hasReachedQueuePosition)
        {
            return;
        }

        lastQueueIndex = newIndex;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveToQueueRoutine());
    }

    IEnumerator MoveToQueueRoutine()
    {
        int newIndex = CustomerManager.Instance.GetQueueIndex(this);

        bool changingPosition = newIndex != lastQueueIndex;

        if (changingPosition)
        {
            hasReachedQueuePosition = false;
        }

        currentState = CustomerState.WalkingToQueue;

        lastQueueIndex = newIndex;

        Vector3 target =
            CustomerManager.Instance.QueuePositions[lastQueueIndex].position;

        yield return MoveTo(target, false);

        if (currentState != CustomerState.WalkingToQueue)
            yield break;

        hasReachedQueuePosition = true;

        currentState = CustomerState.WaitingInQueue;
    }
}
