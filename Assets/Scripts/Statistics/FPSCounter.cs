using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public float timer;
    public float refresh;
    public float avgFramerate;

    string display = "Client FPS: {0}";
    private TMP_Text displayText;

    void Start()
    {
        displayText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        CountFPS();
    }

    void CountFPS()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        displayText.text = string.Format(display, avgFramerate.ToString());
    }
}