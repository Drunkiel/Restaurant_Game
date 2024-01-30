using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool isPlayerNearby;
    [SerializeField] private GameObject hint;
    public UnityEvent onClickFunctionalities;    
    public UnityEvent onHoldClickFunctionalities;    
    public UnityEvent onEndClickFunctionalities;    

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            StartOnClickInteraction();
            return;
        }

        if (isPlayerNearby && Input.GetMouseButton(0))
        {
            StartOnHoldInteraction();
        }

        if (isPlayerNearby && Input.GetKeyUp(KeyCode.F))
        {
            StartOnEndInteraction();
            return;
        }

        if (InteractionSystem.isInteracting) return;

        isPlayerNearby = GetComponent<MultiTriggerController>().isTriggered;
        hint.SetActive(isPlayerNearby);
    }

    private void StartOnClickInteraction()
    {
        onClickFunctionalities.Invoke();
    }

    private void StartOnHoldInteraction()
    {
        onHoldClickFunctionalities.Invoke();
    }

    private void StartOnEndInteraction()
    {
        onEndClickFunctionalities.Invoke();
    }
}
