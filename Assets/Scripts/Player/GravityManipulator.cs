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

    private GravityObject selectedObject;

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

        if (selectedObject != null)
        {
            laser.enabled = true;

            laser.SetPosition(0, gravityOrigin.position);
            laser.SetPosition(1, selectedObject.transform.position);

            SetLaserColor(selectedColor);

            return;
        }

    
        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Vector2 direction =
            (mouse - gravityOrigin.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            gravityOrigin.position,
            direction,
            interactionDistance);

        if(hit.collider != null)
        {
            GravityObject obj =
                hit.collider.GetComponent<GravityObject>();

            if(obj != null)
            {
                laser.enabled = true;

                laser.SetPosition(0, gravityOrigin.position);
                laser.SetPosition(1, obj.transform.position);

                // O objeto está apenas a ser apontado pelo rato
                SetLaserColor(hoverColor);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    selectedObject = obj;

                    // Agora passa a estar selecionado
                    SetLaserColor(selectedColor);
                }

                return;
            }
        }

        laser.enabled = false;
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

    void SetLaserColor(Color color)
    {
        laser.startColor = color;
        laser.endColor = color;
    }
}