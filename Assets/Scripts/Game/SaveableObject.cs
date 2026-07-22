using UnityEngine;

public class SaveableObject : MonoBehaviour
{
    [SerializeField]
    private string objectID;

    public string ObjectID
    {
        get { return objectID; }
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void SetActiveState(bool state)
    {
        gameObject.SetActive(state);
    }
}