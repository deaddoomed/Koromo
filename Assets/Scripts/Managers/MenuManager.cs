using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PanelData
{
    public GameObject panel;
    public Vector3 cameraRotation;
    public Vector3 cameraPosition;
}

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private PanelData[] panels;
    private GameObject currentPanel;

    [Header("Camera")] 
    [SerializeField] private float transitionDuration = 1f;

    private Coroutine cameraRoutine;
    private Transform cameraTransform;

    void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    public void ShowPanel(GameObject panelToShow)
    {
        foreach (var p in panels)
            p.panel.SetActive(false);

        panelToShow.SetActive(true);
        currentPanel = panelToShow;

        HandleCameraForPanel(panelToShow);
    }

    void HandleCameraForPanel(GameObject panel)
    {
        foreach (var p in panels)
        {
            if (p.panel == panel)
            {
                if (cameraRoutine != null)
                    StopCoroutine(cameraRoutine);

                cameraRoutine = StartCoroutine(
                    MoveCamera(
                        p.cameraPosition,
                        Quaternion.Euler(p.cameraRotation)
                    )
                );

                break;
            }
        }
    }

    IEnumerator MoveCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        Vector3 startPosition = cameraTransform.position;
        Quaternion startRotation = cameraTransform.rotation;

        float time = 0f;

        while (time < transitionDuration)
        {
            float t = time / transitionDuration;

            cameraTransform.position = Vector3.Lerp(startPosition, targetPosition, t);
            cameraTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            time += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = targetPosition;
        cameraTransform.rotation = targetRotation;
    }

    public void CreateNewGame()
    {
        GameManager.Instance.NewGame();
    }
}
