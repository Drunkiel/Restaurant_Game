using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public static readonly string savePath = Application.dataPath + "/Saves/";
    public SaveData _data;
    public SettingsData _settingsData;

    void Awake()
    {
        if (!Directory.Exists(Application.dataPath + "/Saves"))
            Directory.CreateDirectory(Application.dataPath + "/Saves");
    }

    public virtual void Save(string path)
    {
        //Here open file
        FileStream saveFile = new(path, FileMode.OpenOrCreate);

        //Here collect data to save

        //Here save data to file
        string jsonData = JsonUtility.ToJson(_settingsData, true);

        saveFile.Close();
        File.WriteAllText(path, jsonData);
    }

    public virtual void Load(string path)
    {
        //Here load data from file
        string saveFile = ReadFromFile(path);
        JsonUtility.FromJsonOverwrite(saveFile, _settingsData);

        //Here override game data
    }

    protected string ReadFromFile(string path)
    {
        using StreamReader Reader = new(path);
        string saveFile = Reader.ReadToEnd();
        return saveFile;
    }


    public static void SaveTextureToFile(Texture2D texture, string path)
    {
        byte[] textureBytes = texture.EncodeToPNG();

        // Saving to file
        File.WriteAllBytes(path, textureBytes);
    }
}