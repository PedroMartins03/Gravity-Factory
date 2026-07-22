using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject btnIdle;    
    [SerializeField] private GameObject doorClosed;
    [SerializeField] private GameObject doorOpen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            if (doorClosed != null) doorClosed.SetActive(false);
            if (doorOpen != null) doorOpen.SetActive(true);
        }
    }
}