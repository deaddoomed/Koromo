using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadPanelController : MonoBehaviour
{
    [SerializeField] private Button[] slotButtons;
    [SerializeField] private string gameSceneName = "GameScene";

    private void OnEnable()
    {
        RefreshSlots();
    }

    void RefreshSlots()
    {
        for (int i = 0; i < slotButtons.Length; i++)
        {
            int slotIndex = i;

            slotButtons[i].onClick.RemoveAllListeners();

            if (SaveSystem.SlotExists(slotIndex))
            {
                SaveMetaData preview = SaveSystem.LoadMetaData(slotIndex);

                if (preview != null)
                {
                    slotButtons[i].GetComponentInChildren<TMP_Text>().text =
                        $"{preview.playerName}\n" +
                        $"Day {preview.dayIndex} - {preview.date:D2}/{preview.month:D2}\n" +
                        $"${preview.money}";

                    slotButtons[i].onClick.AddListener(() => LoadSlot(slotIndex));
                }
                else
                {
                    slotButtons[i].GetComponentInChildren<TMP_Text>().text = "Corrupted Save";
                }
            }
            else
            {
                slotButtons[i].GetComponentInChildren<TMP_Text>().text = "Empty Slot";
            }
        }
    }

    void LoadSlot(int index)
    {
        GameManager.Instance.LoadGame(index);
        SceneManager.LoadScene(gameSceneName);
    }

    public string GetFormattedDate(GameData data)
    {
        return $"Day {data.time.dayIndex} - {data.time.date:D2}/{data.time.month:D2}";
    }
}