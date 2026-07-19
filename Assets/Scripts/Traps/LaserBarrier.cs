using UnityEngine;

public class LaserBarrier : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D laserCollider;
    private bool isDangerous = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        laserCollider = GetComponent<BoxCollider2D>();
        
        if (anim != null)
        {
            anim.SetBool("isActive", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            isDangerous = false;

            if (anim != null)
            {
                anim.SetBool("isActive", false); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            isDangerous = true;

            if (anim != null)
            {
                anim.SetBool("isActive", true); 
            }
        }
    }

    public bool IsDangerous()
    {
        return isDangerous;
    }
}