using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool isPlayerNearby;
    public UnityEvent someFunctionalities;    

    private void Update()
    {
        if (InteractionSystem.isInteracting) return;

        isPlayerNearby = GetComponent<MultiTriggerController>().isTriggered;
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F)) StartInteraction();
    }

    private void StartInteraction()
    {
        someFunctionalities.Invoke();
    }
}
