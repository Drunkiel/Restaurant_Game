using UnityEngine;

public class HintEvent : MonoBehaviour
{
    private int currentHint;
    public bool addOne;

    public void ChangeHint(int eventIndex)
    {
        int index = addOne && eventIndex != 0 ? eventIndex + 1 : eventIndex;
        if (currentHint == index) return;

        currentHint = index;
        HintController.instance.UpdateText(index);
        print("a");
    }
}
