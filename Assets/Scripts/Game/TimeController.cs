using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private int days;
    [SerializeField] private int hours;
    [SerializeField] private int minutes;
    [SerializeField] private float seconds;

    public TMP_Text clockText;

    private void FixedUpdate()
    {
        Timer();
        UpdateClock();
    }

    private void Timer()
    {
        seconds += Time.fixedDeltaTime;

        //Minutes
        if (seconds >= 2)
        {
            minutes += 10;
            seconds = 0;
        }

        //Hours
        if (minutes >= 60)
        {
            minutes -= 60;
            hours++;
        }

        //Add something like end shift
        //if (hours == 16)
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

        string newTime;
        newTime = $"{GetHour()}:{(minutes == 0 ? "00" : minutes.ToString())} {GetMeridiem()}";

        clockText.text = newTime;
    }
}
