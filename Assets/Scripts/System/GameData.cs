using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int saveVersion = 1;

    public PlayerData player;
    public TimeData time;
    public RestaurantData restaurant;

    public GameData()
    {
        player = new PlayerData();
        time = new TimeData();
        restaurant = new RestaurantData();
    }
}

[System.Serializable]
public class PlayerData
{
    public string playerName = "Romina";
    public string gender = "Woman";
    public int tiredness = 4;
    public int money = 0;    
}

[System.Serializable]
public class TimeData
{
    public int playtime = 0;
    public int totalMinutes = 480;
    public int dayIndex = 0;
    public int date = 1;
    public int month = 0;
}

[System.Serializable]
public class RestaurantData
{
    public string restaurantName = "";
    public float stars = 3f;
    public int debt = 0;
}