using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}