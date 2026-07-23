using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    private bool isOpen = false;

    [SerializeField] private string sceneToLoad;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        if (!isOpen){
            isOpen = true;

            if (animator != null){
                animator.SetBool("isActive", true);
            }
            if (audioSource != null && audioSource.clip != null)
            {
            audioSource.Play();
            Debug.Log("A tocar som da porta!"); 
            }
            else
            {
                Debug.LogWarning("O Audio Source ou o Audio Clip estão vazios na porta!");
            }
        }
    }

    public void CloseDoor()
    {
        isOpen = false;
        if (animator != null)
        {
            animator.SetBool("isActive", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}