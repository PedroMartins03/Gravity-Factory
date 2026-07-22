using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] private GameObject computerDeactivated;
    [SerializeField] private GameObject computerActive;
    
    [SerializeField] private GameObject btnIdle; 

    void Start()
    {
        if (computerDeactivated != null) computerDeactivated.SetActive(true);
        if (computerActive != null) computerActive.SetActive(false);
        
        if (btnIdle != null) btnIdle.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (computerDeactivated != null) computerDeactivated.SetActive(false);
            if (computerActive != null) computerActive.SetActive(true);

            if (btnIdle != null) btnIdle.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}