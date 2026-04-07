using System;
using UnityEngine;

public class CalendarSystem
{
    private GameData data;

    private readonly int[] lastDay = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    public static event Action OnNewDay;

    public CalendarSystem(GameData gameData)
    {
        data = gameData;
        ClockSystem.OnDayPassed += AdvanceDay;
    }

    void AdvanceDay()
    {
        Debug.Log("cambie de dia!");

        data.time.date += 1;
        data.time.dayIndex = (data.time.dayIndex + 1) % 7;

        if (data.time.date > lastDay[data.time.month])
        {
            data.time.date = 1;
            data.time.month = (data.time.month + 1) % 12;
        }

        OnNewDay?.Invoke();
    }
}
