using UnityEngine;

public class HintEvent : MonoBehaviour
{
    public void UseHint(int eventIndex)
    {
        HintController.instance.UpdateText(eventIndex);
    }
}
