using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadGameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject loadGameImage;

    [SerializeField]
    private GameObject mainMenuPanel;


    [SerializeField]
    private TMP_Text slot1Status;

    [SerializeField]
    private TMP_Text slot2Status;

    [SerializeField]
    private TMP_Text slot3Status;


    public void OpenLoadGameMenu()
    {
        UpdateSlotStatus();

        mainMenuPanel.SetActive(false);
        loadGameImage.SetActive(true);
    }

    public void CloseLoadGameMenu()
    {
        loadGameImage.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void LoadFromSlot(int slot)
    {
        if (!SaveSystem.SaveExists(slot))
        {
            Debug.Log("Este slot está vazio.");
            return;
        }

        SaveData data =
            SaveSystem.Load(slot);

        if (data == null)
        {
            Debug.Log("Não foi possível carregar o save.");
            return;
        }

        ContinueGame.saveSlotToLoad = slot;

        SceneManager.LoadScene(data.levelName);
    }


    private void UpdateSlotStatus()
    {
        UpdateSlot(
            1,
            slot1Status
        );

        UpdateSlot(
            2,
            slot2Status
        );

        UpdateSlot(
            3,
            slot3Status
        );
    }

    private void UpdateSlot(
        int slot,
        TMP_Text statusText
    )
    {
        if (!SaveSystem.SaveExists(slot))
        {
            statusText.text =
                "EMPTY SLOT";

            return;
        }

        SaveData data =
            SaveSystem.Load(slot);

        statusText.text =
            data.levelName
            + "\n"
            + data.saveDate;
    }
}   