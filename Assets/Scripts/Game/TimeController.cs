using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class SkyColor
{
    public Color layer0;
    public Color layer1;
    public Color layer2;
}

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    [SerializeField] private int days;
    [SerializeField] private int hours;
    [SerializeField] private int minutes;
    [SerializeField] private float seconds;

    [SerializeField] private Light skyLight;
    [SerializeField] private Material skyMaterial;
    [SerializeField] private List<SkyColor> skyColors = new();

    public TMP_Text clockText;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private Animator dayTextAnimator;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (!GameController.isGameStarted)
            return;

        Timer();
        UpdateClock();
    }

    private void Timer()
    {
        seconds += Time.fixedDeltaTime;

        //Minutes
        if (seconds >= 1)
        {
            minutes += 1;
            seconds -= 1;
        }

        //Hours
        if (minutes >= 60)
        {
            minutes -= 60;
            hours++;

            if (hours == 8)
                skyLight.GetComponent<Animator>().Play($"Light_State{0}");

            if (hours == 12)
                skyLight.GetComponent<Animator>().Play($"Camera_State{2}");

            if (hours == GameController.endWorkHour)
            {
                skyLight.GetComponent<Animator>().Play($"Light_State{1}");
                skyLight.GetComponent<Animator>().Play($"Camera_State{1}");
                clockText.GetComponent<Animator>().SetBool("isBlinking", true);
                ShowText("Closing time!");
            }

            if (hours == 20)
            {
                skyLight.GetComponent<Animator>().Play($"Light_State{2}");
                skyLight.GetComponent<Animator>().Play($"Camera_State{0}");
            }
        }

        //Days
        if (hours >= 24)
        {
            hours -= 24;
            days++;
        }
    }

    public int GetDay()
    {
        return days;
    }

    public Vector3 GetTime()
    {
        return new Vector3(hours, minutes, seconds);
    }

    public void ShowText(string newText)
    {
        dayText.text = newText;
        dayTextAnimator.SetTrigger("Trigger");
    }

    public void NewDay()
    {
        days++;
        hours = 8;
        minutes = 0;
        seconds = 0;
    }

    private void UpdateClock()
    {
        string GetMeridiem()
        {
            return (hours < 12) ? "AM" : "PM";
        }

        int GetHour()
        {
            return (hours % 12 == 0) ? 12 : hours % 12;
        }

        string GetMinute()
        {
            if (minutes == 0)
                return "00";

            if (minutes < 10)
                return "0" + minutes;

            return minutes.ToString();
        }

        string newTime;
        newTime = $"{GetHour()}:{GetMinute()} {GetMeridiem()}";

        clockText.text = newTime;
    }
}
