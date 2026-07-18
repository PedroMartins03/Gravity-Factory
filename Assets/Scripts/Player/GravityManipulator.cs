using UnityEngine;

public class GravityManipulator : MonoBehaviour
{
    [Header("References")]
    public Transform gravityOrigin;

    [SerializeField]
    private LineRenderer laser;

    [Header("Settings")]
    public float interactionDistance = 6f;

    [SerializeField] private Color hoverColor = Color.cyan;
    [SerializeField] private Color selectedColor = Color.green;


    [SerializeField] private LayerMask interactionLayers;

    private GravityObject selectedObject;


    [SerializeField]
    private float laserWaveAmplitude = 0.03f;

    [SerializeField]
    private float laserWaveSpeed = 8f;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {

        if (selectedObject != null && Input.GetKeyDown(KeyCode.E))
        {
            selectedObject.Stop();

            selectedObject = null;

            laser.enabled = false;

            return;
        }
        
        DetectObject();

        if (selectedObject != null)
        {
            HandleGravityInput();
        }
    }

    void DetectObject()
    {
        // OBJETO JÁ SELECIONADO
        if (selectedObject != null)
        {
            laser.enabled = true;

            AnimateLaser(
                gravityOrigin.position,
                selectedObject.transform.position
            );

            SetLaserColor(selectedColor);

            return;
        }


        // DETETAR O QUE ESTÁ DEBAIXO DO RATO

        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector2 direction = (mouse - transform.position).normalized;

        float raycastOffset = 0.6f;

        Vector2 raycastOrigin = (Vector2)transform.position + direction * raycastOffset;

        RaycastHit2D hit = Physics2D.Raycast(
            raycastOrigin,
            direction,
            interactionDistance
        );


        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<GravityObject>() == null)
            {
                laser.enabled = false;
                return;
            }

            GravityObject obj =
                hit.collider.GetComponent<GravityObject>();

            laser.enabled = true;

            AnimateLaser(
                gravityOrigin.position,
                obj.transform.position
            );

            SetLaserColor(hoverColor);

            if (Input.GetKeyDown(KeyCode.E))
            {
                selectedObject = obj;

                SetLaserColor(selectedColor);
            }

            return;
        }

        laser.enabled = false;
    }


    void AnimateLaser(Vector2 start, Vector2 end)
    {
        int pointCount = laser.positionCount;

        Vector2 direction = end - start;

        Vector2 perpendicular =
            new Vector2(-direction.y, direction.x).normalized;

        for(int i = 0; i < pointCount; i++)
        {
            float t = (float)i / (pointCount - 1);

            Vector2 position =
                Vector2.Lerp(start, end, t);

            float wave =
                Mathf.Sin(Time.time * laserWaveSpeed + t * 10f)
                * laserWaveAmplitude;

            position += perpendicular * wave;

            laser.SetPosition(i, position);
        }
    }

    void SetLaserColor(Color color)
    {
        laser.startColor = color;
        laser.endColor = color;
    }


    void HandleGravityInput()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            direction = Vector2.up;

        else if (Input.GetKey(KeyCode.DownArrow))
            direction = Vector2.down;

        else if (Input.GetKey(KeyCode.LeftArrow))
            direction = Vector2.left;

        else if (Input.GetKey(KeyCode.RightArrow))
            direction = Vector2.right;

        selectedObject.Move(direction);

        if (direction == Vector2.zero)
        {
            selectedObject.Stop();
        }
    }
}