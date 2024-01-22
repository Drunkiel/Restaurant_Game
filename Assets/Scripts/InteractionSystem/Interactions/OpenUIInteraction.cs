using UnityEngine;

public class OpenUIInteraction : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] private int indexOfUI;

    public void OpenUI()
    {
        isOpen = !isOpen;
        InteractionSystem.instance.allUI[indexOfUI].SetActive(isOpen);
        InteractionSystem.isInteracting = isOpen;
    }
}
