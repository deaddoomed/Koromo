using UnityEngine;
using System;

public class TimeSystem
{
    private float accumulator;
    private GameData data;
    private int lastHour = -1;
    private int lastMinute = -1;

    private readonly int[] lastDay = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    public static event Action<int> OnHourChanged;
    public static event Action<int,int> OnMinuteChanged;
    public static event Action OneNewDay;

    public TimeSystem (GameData gameData)
    {
        data = gameData;
    }

    public void AdvanceTime(float deltaTime, int timeSpeed)
    {
        accumulator += deltaTime*timeSpeed;
        
        while (accumulator >= 1f)
        {
            accumulator -= 1f;
            data.time.totalMinutes += 15; //amount of minutes the clock advances

            if (data.time.totalMinutes >= 1440)
            {
                Debug.Log("cambie de dia!");
                Debug.Log(data.time.date);
                data.time.totalMinutes -= 1440;
                data.time.date += 1;
                data.time.dayIndex = (data.time.dayIndex + 1) % 7;
              
                if (data.time.date > lastDay[data.time.month])
                {
                    data.time.date = 1;
                    data.time.month = (data.time.month +1) % 12; 
                }
                OneNewDay?.Invoke();
            } 

            int hour = GetHour();
            int minute = GetMinute();

            if (minute != lastMinute)
            {
                lastMinute = minute;
                OnMinuteChanged?.Invoke(hour,minute);
            }

            if (hour != lastHour)
            {
                lastHour = hour;
                OnHourChanged?.Invoke(hour);
            }
        }
    }

    public int GetHour() => data.time.totalMinutes/60;
    public int GetMinute() => data.time.totalMinutes%60;
}
