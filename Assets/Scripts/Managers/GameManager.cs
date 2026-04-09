using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameData data = new GameData();
    public int currentSlot = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene("MainMenu");
    }

    public void SaveGame(int slotIndex)
    {
        currentSlot = slotIndex;
        SaveSystem.SaveGame(data, currentSlot);
    }

    public void LoadGame(int slotIndex)
    {
        GameData loadedData = SaveSystem.LoadGame(slotIndex);

        if (loadedData != null)
        {
            currentSlot = slotIndex;
            data = loadedData;
            TimeController.Instance.SetGameData(data);
            TimeController.Instance.SetTimeSpeed(1);
            TimeController.Instance.StartTime();
            Debug.Log("Juego cargado");
        }
    }

    public void NewGame()
    {
        currentSlot = -1;
        data = new GameData();
        TimeController.Instance.SetGameData(data);
        TimeController.Instance.SetTimeSpeed(1);
        TimeController.Instance.StartTime(); 
        SceneManager.LoadScene("Restaurant");   
    }
}