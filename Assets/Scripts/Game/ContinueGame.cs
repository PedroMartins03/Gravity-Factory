using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour
{
    public static int saveSlotToLoad = -1;

    public void Continue()
    {
        int lastSaveSlot =
            SaveSystem.GetLastSaveSlot();

        if (lastSaveSlot == -1)
        {
            return;
        }

        SaveData data =
            SaveSystem.Load(lastSaveSlot);

        if (data == null)
        {
            return;
        }

        saveSlotToLoad = lastSaveSlot;

        SceneManager.LoadScene(data.levelName);
    }
}