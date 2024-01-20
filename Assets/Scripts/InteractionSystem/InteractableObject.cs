using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool isPlayerNearby;
    [SerializeField] private GameObject hint;
    public UnityEvent someFunctionalities;    

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F)) StartInteraction();
        if (InteractionSystem.isInteracting) return;

        isPlayerNearby = GetComponent<MultiTriggerController>().isTriggered;
        hint.SetActive(isPlayerNearby);
    }

    private void StartInteraction()
    {
        someFunctionalities.Invoke();
    }
}
