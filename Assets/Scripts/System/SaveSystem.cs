using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class SaveMetaData
{
    public string playerName;
    public int dayIndex;
    public int date;
    public int month;
    public int money;
    public string restaurantName;
    public float stars;

    public SaveMetaData() {}

    public SaveMetaData(GameData data)
    {
        if (data == null) return;

        playerName = data.player.playerName;
        dayIndex = data.time.dayIndex;
        date = data.time.date;
        month = data.time.month;
        money = data.player.money;
        restaurantName = data.restaurant.restaurantName;
        stars = data.restaurant.stars;
    }
}

[System.Serializable]
public class SaveFile
{
    public int version =1;
    public SaveMetaData meta;
    public GameData data;
}

public static class SaveSystem
{
    private static string GetPath(int slotIndex)
    {
        return Path.Combine(Application.persistentDataPath, $"save_{slotIndex}.json");
    }

    public static void SaveGame(GameData data, int slotIndex)
    {
        SaveFile save = new SaveFile();
        save.meta = new SaveMetaData(data);
        save.data = data;

        string json = JsonConvert.SerializeObject(save, Formatting.Indented);

        string path = GetPath(slotIndex);
        string tempPath = path + ".tmp";

        File.WriteAllText(tempPath, json);

        if (File.Exists(path))
            File.Delete(path);

        File.Move(tempPath, path);
    }

    public static GameData LoadGame(int slotIndex)
    {
        string path = GetPath(slotIndex);
        string tempPath = path + ".tmp";

        if (File.Exists(tempPath))
            File.Delete(tempPath);

        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        SaveFile save = JsonConvert.DeserializeObject<SaveFile>(json, settings);
        GameData data = save.data;

        if (data.player == null) data.player = new PlayerData();
        if (data.time == null) data.time = new TimeData();
        if (data.restaurant == null) data.restaurant = new RestaurantData();

        return data;
    }

    public static SaveMetaData LoadMetaData(int slotIndex)
    {
        string path = GetPath(slotIndex);

        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);

        SaveFile save = JsonConvert.DeserializeObject<SaveFile>(json);

        return save.meta;
    }

    public static bool SlotExists(int slotIndex)
    {
        return File.Exists(GetPath(slotIndex));
    }

    public static void DeleteSlot(int slotIndex)
    {
        string path = GetPath(slotIndex);

        if (File.Exists(path))
            File.Delete(path);
    }
}