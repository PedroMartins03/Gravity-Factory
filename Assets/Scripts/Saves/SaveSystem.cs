using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string GetSavePath(int slot)
    {
        return Path.Combine(
            Application.persistentDataPath,
            "save_" + slot + ".json"
        );
    }

    public static void Save(
        int slot,
        Transform player,
        GravityObject[] gravityObjects
    )
    {
        SaveData data = new SaveData();

        data.saveDate = DateTime.Now.ToString();

        data.levelName =
            UnityEngine.SceneManagement.SceneManager
            .GetActiveScene()
            .name;

        data.playerPositionX =
            player.position.x;

        data.playerPositionY =
            player.position.y;

        data.gravityBoxes =
            new GravityBoxSaveData[
                gravityObjects.Length
            ];

        for (int i = 0; i < gravityObjects.Length; i++)
        {
            data.gravityBoxes[i] =
                new GravityBoxSaveData();

            data.gravityBoxes[i].boxName =
                gravityObjects[i].name;

            data.gravityBoxes[i].positionX =
                gravityObjects[i].transform.position.x;

            data.gravityBoxes[i].positionY =
                gravityObjects[i].transform.position.y;
        }

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(
            GetSavePath(slot),
            json
        );

        PlayerPrefs.SetInt("LastSaveSlot", slot);
        PlayerPrefs.Save();

        Debug.Log("Save " + slot + " guardado.");
    }

    public static bool SaveExists(int slot)
    {
        return File.Exists(GetSavePath(slot));
    }

    public static void DeleteSave(int slot)
    {
        string path = GetSavePath(slot);

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        Debug.Log("Save " + slot + " apagado.");
    }

    public static int GetLastSaveSlot()
    {
        return PlayerPrefs.GetInt("LastSaveSlot", -1);
    }

    public static void DeleteAllSaves()
    {
        DeleteSave(1);
        DeleteSave(2);
        DeleteSave(3);

        PlayerPrefs.DeleteKey("LastSaveSlot");
        PlayerPrefs.Save();

        Debug.Log("Todos os saves foram apagados.");
    }


    public static SaveData Load(int slot)
    {
        string path = GetSavePath(slot);

        if (!File.Exists(path))
        {
            return null;
        }

        string json = File.ReadAllText(path);

        SaveData data =
            JsonUtility.FromJson<SaveData>(json);

        return data;
    }


    public static void LoadPlayerPosition(
        int slot,
        Transform player
    )
    {
        SaveData data = Load(slot);

        if (data == null)
        {
            Debug.Log("Não existe nenhum save nesse slot.");
            return;
        }

        player.position = new Vector3(
            data.playerPositionX,
            data.playerPositionY,
            player.position.z
        );

        Debug.Log(
            "Posição do Player carregada do Save " + slot
        );
    }


    public static void LoadGravityObjects(
        int slot,
        GravityObject[] gravityObjects
    )
    {
        SaveData data = Load(slot);

        if (data == null)
        {
            Debug.Log("Não existe nenhum save nesse slot.");
            return;
        }

        foreach (GravityBoxSaveData savedBox in data.gravityBoxes)
        {
            foreach (GravityObject gravityObject in gravityObjects)
            {
                if (gravityObject.name == savedBox.boxName)
                {
                    gravityObject.transform.position =
                        new Vector3(
                            savedBox.positionX,
                            savedBox.positionY,
                            gravityObject.transform.position.z
                        );

                    break;
                }
            }
        }

        Debug.Log(
            "Posições das Gravity Objects carregadas."
        );
    }

}

[Serializable]
public class SaveData
{
    public string saveDate;
    public string levelName;

    public float playerPositionX;
    public float playerPositionY;

    public GravityBoxSaveData[] gravityBoxes;
}


[Serializable]
public class GravityBoxSaveData
{
    public string boxName;

    public float positionX;
    public float positionY;
}