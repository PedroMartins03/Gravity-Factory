using UnityEngine;

public class GravityObject : Interactable
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float smoothTime = 0.08f;

    private Vector2 targetVelocity;
    private Vector2 currentVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.SmoothDamp(
            rb.linearVelocity,
            targetVelocity,
            ref currentVelocity,
            smoothTime);
    }

    public void Move(Vector2 direction)
    {
        targetVelocity = direction * moveSpeed;
    }

    public void Stop()
    {
        targetVelocity = Vector2.zero;
    }
}