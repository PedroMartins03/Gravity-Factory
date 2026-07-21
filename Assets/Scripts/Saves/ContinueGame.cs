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
            Debug.Log("Não existe nenhum save para continuar.");
            return;
        }

        SaveData data =
            SaveSystem.Load(lastSaveSlot);

        if (data == null)
        {
            Debug.Log("O save não foi encontrado.");
            return;
        }

        saveSlotToLoad = lastSaveSlot;

        SceneManager.LoadScene(data.levelName);
    }
}