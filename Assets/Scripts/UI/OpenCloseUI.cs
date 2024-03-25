using UnityEngine;

public class OpenCloseUI : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    public GameObject UI;

    public void OpenClose()
    {
        isOpen = !isOpen;
        UI.SetActive(isOpen);
    }
}
