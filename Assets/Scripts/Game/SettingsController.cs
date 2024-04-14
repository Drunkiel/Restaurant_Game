using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : SaveLoadSystem
{
    public static SettingsController instance;

    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public TMP_InputField fpsInput;
    public Toggle vSyncToggle;
    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;

    private readonly List<Vector2Int> resolutions = new() { new(3840, 2160), new(2560, 1440), new(1920, 1080), new(1280, 720) };

    private void Awake()
    {
        instance = this;

        try
        {
            Load(savePath + "settings.json");
        }
        catch (System.Exception)
        {
            Save(savePath + "settings.json");
        }
    }

    public override void Save(string path)
    {
        path = savePath + "settings.json";
        
        //Creating or open file
        FileStream saveFile = new(path, FileMode.OpenOrCreate);

        //Overrite data to save
        //PLAYER SETTINGS
        _settingsData.resolutionIndex = resolutionDropdown.value;
        _settingsData.fullscreenOn = fullscreenToggle.isOn;
        _settingsData.maxFPS = int.Parse(fpsInput.text);
        _settingsData.vSync = vSyncToggle.isOn;
        _settingsData.qualityIndex = qualityDropdown.value;
        _settingsData.volume = volumeSlider.value;

        //Saving data
        string jsonData = JsonUtility.ToJson(_settingsData, true);

        saveFile.Close();
        File.WriteAllText(path, jsonData);
    }

    public override void Load(string path)
    {
        path = savePath + "settings.json";

        //Load data from file
        string saveFile = ReadFromFile(path);
        JsonUtility.FromJsonOverwrite(saveFile, _settingsData);

        //LOAD SETTINGS
        resolutionDropdown.value = _settingsData.resolutionIndex;
        fullscreenToggle.isOn = _settingsData.fullscreenOn;
        fpsInput.text = _settingsData.maxFPS.ToString();
        vSyncToggle.isOn = _settingsData.vSync;
        qualityDropdown.value = _settingsData.qualityIndex;
        volumeSlider.value = _settingsData.volume;
    }

    public void UpdateQuality()
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    public void UpdateVSync()
    {
        if (vSyncToggle.isOn)
            QualitySettings.vSyncCount = 1;
        else
        {
            QualitySettings.vSyncCount = 0;
            UpdateFPS();
        }
    }

    public void UpdateResolution()
    {
        int width = resolutions[resolutionDropdown.value].x;
        int height = resolutions[resolutionDropdown.value].y;

        Screen.SetResolution(width,
                            height,
                            fullscreenToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
    }

    public void UpdateFPS()
    {
        int newMaxFPS = int.Parse(fpsInput.text);
        if (newMaxFPS < 30)
        {
            fpsInput.text = "30";
            newMaxFPS = 30;
        }

        if (QualitySettings.vSyncCount != 0)
            return;

        Application.targetFrameRate = newMaxFPS;
    }
}
