using UnityEngine;

public class HintEvent : MonoBehaviour
{
    private int currentHint;
    public bool addOne;
    public bool reverse;

    public void ChangeHint(int eventIndex)
    {
        bool Check()
        {
            if (reverse)
                return !addOne;
            else
                return addOne;
        }

        int index = Check() && eventIndex != 0 ? eventIndex + 1 : eventIndex;
        if (currentHint == index) return;

        currentHint = index;
        HintController.instance.UpdateText(index);
    }
}
