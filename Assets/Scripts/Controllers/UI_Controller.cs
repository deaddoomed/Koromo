using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI starsText;

    [Header("Time UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI dayPartText;

    //private GameData data;

    private readonly List<string> days = new List<string>
                    { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday","Sunday" };
    private readonly List<string> months = new List<string>
                    { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    //void Awake()
    //{
    //    data = GameManager.Instance.data;
    //}

    private GameData data => GameManager.Instance.data;

    private void Start()
    {
        UpdateUI();

        int hour = data.time.totalMinutes / 60;
        int minute = data.time.totalMinutes % 60;

        UpdateTime(hour, minute);
        UpdateDayPart(hour);
        UpdateDate();
    }

    public void UpdateUI()
    {
        nameText.text = data.player.playerName;
        moneyText.text = "$" + data.player.money.ToString();
        starsText.text = data.restaurant.stars.ToString("0.0") + " stars";
    }

    private void OnEnable()
    {
        TimeSystem.OnMinuteChanged += UpdateTime;
        TimeSystem.OnHourChanged += UpdateDayPart;
        TimeSystem.OneNewDay += UpdateDate;
    }
    private void OnDisable()
    {
        TimeSystem.OnMinuteChanged -= UpdateTime;
        TimeSystem.OnHourChanged -= UpdateDayPart;
        TimeSystem.OneNewDay -= UpdateDate;
    }

    private void UpdateTime(int hour, int minute)
    {
        timerText.text = $"{hour:00}:{minute:00}";
    }

    private void UpdateDayPart(int hour)
    {
        if (hour >= 6 && hour < 12)
            dayPartText.text = "Morning";
        else if (hour >= 12 && hour < 20)
            dayPartText.text = "Afternoon";
        else
            dayPartText.text = "Night";
    }

    private void UpdateDate()
    {
        Debug.Log("UI DATE UPDATED");

        dayText.text = days[data.time.dayIndex];
        dateText.text = $"{months[data.time.month]} {data.time.date}";
    }
}
