using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//HOLD: functions are disabled until I find way to make them work with the new Input system or find the other way

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool isPlayerNearby;
    [SerializeField] private GameObject hint;
    public UnityEvent onClickFunctionalities;
    //public UnityEvent onHoldClickFunctionalities;
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

/*    public void Hold(InputAction.CallbackContext context)
    {
        if (isPlayerNearby) StartOnHoldInteraction();
    }*/

    public void End(InputAction.CallbackContext context)
    {
        if (isPlayerNearby && context.canceled) StartOnEndInteraction();
    }

    private void StartOnClickInteraction()
    {
        onClickFunctionalities.Invoke();
    }

/*    private void StartOnHoldInteraction()
    {
        onHoldClickFunctionalities.Invoke();
    }*/

    private void StartOnEndInteraction()
    {
        onEndClickFunctionalities.Invoke();
    }
}
