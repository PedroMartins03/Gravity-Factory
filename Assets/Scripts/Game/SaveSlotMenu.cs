using UnityEngine;
using TMPro;

public class SaveSlotMenu : MonoBehaviour
{

    [Header("Player")]
    [SerializeField]
    private Transform player;

    [Header("Gravity Objects")]
    [SerializeField]
    private GravityObject[] gravityObjects;


    [Header("Panels")]
    [SerializeField] private GameObject saveSlotPanel;
    [SerializeField] private GameObject pauseWindow;

    [Header("Slot Status Text")]
    [SerializeField] private TMP_Text slot1Status;
    [SerializeField] private TMP_Text slot2Status;
    [SerializeField] private TMP_Text slot3Status;

    public void OpenSaveSlotMenu()
    {
        pauseWindow.SetActive(false);
        saveSlotPanel.SetActive(true);

        UpdateSlotStatus();
    }

    public void CloseSaveSlotMenu()
    {
        saveSlotPanel.SetActive(false);
        pauseWindow.SetActive(true);
    }

    public void SaveToSlot(int slot)
    {
        SaveSystem.Save(
            slot,
            player,
            gravityObjects
        );

        Debug.Log(
            "Jogo guardado no Save " + slot
        );

        UpdateSlotStatus();
    }

    private void UpdateSlotStatus()
    {
        UpdateSlotText(
            slot1Status,
            1
        );

        UpdateSlotText(
            slot2Status,
            2
        );

        UpdateSlotText(
            slot3Status,
            3
        );
    }

    private void UpdateSlotText(
        TMP_Text statusText,
        int slot
    )
    {
        if (SaveSystem.SaveExists(slot))
        {
            SaveData data =
                SaveSystem.Load(slot);

            statusText.text =
                data.levelName +
                "\n" +
                data.saveDate;
        }
        else
        {
            statusText.text = "EMPTY SLOT";
        }
    }


    public void LoadFromSlot(int slot)
    {
        SaveSystem.LoadPlayerPosition(
            slot,
            player
        );

        SaveSystem.LoadGravityObjects(
            slot,
            gravityObjects
        );

        Debug.Log(
            "Save " + slot + " carregado."
        );
    }
}