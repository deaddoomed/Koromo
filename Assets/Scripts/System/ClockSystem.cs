using System;

public class ClockSystem
{
    private float accumulator;
    private GameData data;

    private int lastHour = -1;
    private int lastMinute = -1;

    public static event Action<int> OnHourChanged;
    public static event Action<int,int> OnMinuteChanged;
    public static event Action OnDayPassed;

    public ClockSystem(GameData gameData)
    {
        data = gameData;
    }

    public void AdvanceTime(float deltaTime, int timeSpeed)
    {
        accumulator += deltaTime * timeSpeed;

        while (accumulator >= 1f)
        {
            accumulator -= 1f;

            data.time.totalMinutes += 15;

            if (data.time.totalMinutes >= 1440)
            {
                data.time.totalMinutes -= 1440;
                OnDayPassed?.Invoke();
            }

            int hour = GetHour();
            int minute = GetMinute();

            if (minute != lastMinute)
            {
                lastMinute = minute;
                OnMinuteChanged?.Invoke(hour, minute);
            }

            if (hour != lastHour)
            {
                lastHour = hour;
                OnHourChanged?.Invoke(hour);
            }
        }
    }

    public int GetHour() => data.time.totalMinutes / 60;
    public int GetMinute() => data.time.totalMinutes % 60;
}
