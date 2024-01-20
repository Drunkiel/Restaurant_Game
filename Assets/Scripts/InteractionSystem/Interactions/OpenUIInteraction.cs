using UnityEngine;

public class OpenUIInteraction : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] private GameObject UI;

    public void OpenUI()
    {
        isOpen = !isOpen;
        UI.SetActive(isOpen);
        InteractionSystem.isInteracting = isOpen;
    }
}
