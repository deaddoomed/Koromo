using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private int timeSpeed = 5;
    
    private bool isRunning = false;
    private TimeSystem timeSystem; 

    public static TimeController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        timeSystem = new TimeSystem(GameManager.Instance.data);
    }

    void Update()
    {
        if(!isRunning) return;
        timeSystem.AdvanceTime(Time.deltaTime, timeSpeed);
    }

    public void SetGameData(GameData data)
    {
        timeSystem = new TimeSystem(data);
    }

    public void SetTimeSpeed( int newSpeed)
    {
        timeSpeed = newSpeed;
    }

    public void StartTime()
    {
        isRunning = true;
    }

    public void PauseTime()
    {
        isRunning = false;
    }
}
