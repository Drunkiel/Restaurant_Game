using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool isPlayerNearby;
    [SerializeField] private GameObject hint;
    public UnityEvent onClickFunctionalities;
    public UnityEvent onHoldFunctionalities;
    public UnityEvent onEndClickFunctionalities;

    private void Update()
    {
        if (InteractionSystem.isInteracting) return;

        isPlayerNearby = GetComponent<MultiTriggerController>().isTriggered;
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (isPlayerNearby && context.performed) StartOnClickInteraction();
    }

    public void Hold(InputAction.CallbackContext context)
    {
        if (isPlayerNearby && context.performed) StartOnHoldInteraction();
    }

    public void End(InputAction.CallbackContext context)
    {
        if (isPlayerNearby && context.canceled) StartOnEndInteraction();
    }

    private void StartOnClickInteraction()
    {
        onClickFunctionalities.Invoke();
    }

    private void StartOnHoldInteraction()
    {
        onHoldFunctionalities.Invoke();
    }

    private void StartOnEndInteraction()
    {
        onEndClickFunctionalities.Invoke();
    }
}
