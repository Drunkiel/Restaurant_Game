using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorVariable : MonoBehaviour
{
    private byte colorValue;
    [SerializeField] private Slider colorSlider;
    [SerializeField] private TMP_InputField valueInput;

    private void Start()
    {
        UpdateColorByInput();
    }

    public byte GetValue()
    {
        return colorValue;
    }

    public void SetValue(byte value)
    {
        colorSlider.SetValueWithoutNotify(value);
        valueInput.text = value.ToString();
        colorValue = value;
    }

    public void UpdateColorBySlider()
    {
        valueInput.SetTextWithoutNotify(colorSlider.value.ToString());
        colorValue = byte.Parse(colorSlider.value.ToString());
    }

    public void UpdateColorByInput()
    {
        if (float.TryParse(valueInput.text, out float newValue))
        {
            colorSlider.SetValueWithoutNotify(newValue);
            colorValue = byte.Parse(colorSlider.value.ToString()); ;
        }
        else
            SetValue(0);
    }
}
