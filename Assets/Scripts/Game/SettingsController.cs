using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static SettingsController instance;

    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public TMP_InputField fpsInput;
    //public Toggle vSyncToggle;
    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;

    private readonly List<Vector2Int> resolutions = new() { new(3840, 2160), new(2560, 1440), new(1920, 1080), new(1280, 720) }; 

    private void Awake()
    {
        instance = this;
    }

    public void UpdateResolution()
    {
        int width = resolutions[resolutionDropdown.value].x;
        int height = resolutions[resolutionDropdown.value].y;

        Screen.SetResolution(width,
                            height,
                            fullscreenToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed,
                            new RefreshRate()
                            { 
                                numerator = uint.Parse(fpsInput.text), denominator = 1 
                            });
    }
}
