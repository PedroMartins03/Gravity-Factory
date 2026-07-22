using UnityEngine;

public class LoadSaveOnLevelStart : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GravityObject[] gravityObjects;

    [Header("Saveable Objects (Optional)")]
    [SerializeField]
    private SaveableObject[] saveableObjects;


    private void Start()
    {
        if (
            ContinueGame.saveSlotToLoad ==
            -1
        )
        {
            return;
        }

        int slot =
            ContinueGame.saveSlotToLoad;


        SaveSystem.LoadPlayerPosition(
            slot,
            player
        );


        SaveSystem.LoadGravityObjects(
            slot,
            gravityObjects
        );


        SaveSystem.LoadSaveableObjects(
            slot,
            saveableObjects
        );


        ContinueGame.saveSlotToLoad =
            -1;


        Debug.Log(
            "Save carregado ao iniciar o nível."
        );
    }
}