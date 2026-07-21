using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float topY = 5f;    
    [SerializeField] private float bottomY = 0f; 
    [SerializeField] private float speed = 3f;   

    private bool movingUp = true;

    void Update()
    {
        if (movingUp)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (transform.position.y >= topY) movingUp = false;
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            if (transform.position.y <= bottomY) movingUp = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}