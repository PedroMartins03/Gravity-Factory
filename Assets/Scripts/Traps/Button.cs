using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Door doorScript; 

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            if (doorScript != null)
                doorScript.OpenDoor();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            if (doorScript != null)
                doorScript.CloseDoor();
        }
    }
}