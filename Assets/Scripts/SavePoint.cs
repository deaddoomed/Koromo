using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private int slotIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaveGame();
        }
    }

    void SaveGame()
    {
        if (GameManager.Instance == null)
            return;

        GameManager.Instance.SaveGame(slotIndex);
        Debug.Log("Juego guardado en slot " + slotIndex);
    }
}
