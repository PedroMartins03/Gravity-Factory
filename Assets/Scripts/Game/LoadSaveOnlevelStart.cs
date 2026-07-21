using UnityEngine;

public class LoadSaveOnLevelStart : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GravityObject[] gravityObjects;

    private void Start()
    {
        if (ContinueGame.saveSlotToLoad == -1)
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

        ContinueGame.saveSlotToLoad = -1;

        Debug.Log(
            "Save carregado ao iniciar o nível."
        );
    }
}