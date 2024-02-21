using TMPro;
using UnityEngine;

public class HintController : MonoBehaviour
{
    public static HintController instance;
    private int currentHint;
    public string[] hints;

    [SerializeField] private TMP_Text hintText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateText(int hintIndex)
    {
        if (hintIndex == currentHint) return;

        currentHint = hintIndex;
        hintText.text = hints[hintIndex];
    }
}
