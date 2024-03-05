using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private int days;
    [SerializeField] private int hours;
    [SerializeField] private int minutes;
    [SerializeField] private float seconds;

    [SerializeField] private TMP_Text clockHour;

    private void FixedUpdate()
    {
        Timer();
        UpdateClock();
    }

    private void Timer()
    {
        seconds += Time.fixedDeltaTime;

        //Minutes
        if (seconds >= 10)
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
        string NewTime;

        if (minutes == 0)
            NewTime = hours.ToString() + "," + "00";
        else
            NewTime = hours.ToString() + "," + minutes.ToString();

        clockHour.text = NewTime;
    }
}
