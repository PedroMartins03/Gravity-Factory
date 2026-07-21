using UnityEngine;

public class ClearSaves : MonoBehaviour
{
    void Start()
    {
        SaveSystem.DeleteAllSaves();

        Destroy(gameObject);
    }
}