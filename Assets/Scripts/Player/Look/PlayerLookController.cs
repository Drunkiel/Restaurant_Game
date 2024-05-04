using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    public static PlayerLookController instance;

    public Texture2D playerTexture;
    public ColorPickerController _colorPicker;

    private void Awake()
    {
        instance = this;
    }
}
