using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    [SerializeField] private int days;
    [SerializeField] private int hours;
    [SerializeField] private int minutes;
    [SerializeField] private float seconds;

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
        if (seconds >= 1.5f)
        {
            minutes += 1;
            seconds = 0;
        }

        //Hours
        if (minutes >= 60)
        {
            minutes -= 60;
            hours++;
        }

        //Days
        if (hours >= 24)
        {
            hours -= 24;
            days++;
        }
    }

    public Vector3 GetTime()
    {
        return new Vector3(hours, minutes, seconds);
    }

    public void ShowDay()
    {
        dayText.text = $"Day {days}";
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
