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

        // POSIÇÃO DO RATO

        Vector3 mouse =
            cam.ScreenToWorldPoint(Input.mousePosition);

        mouse.z = 0f;


        // DIREÇÃO DO PLAYER ATÉ AO RATO

        Vector2 direction =
            ((Vector2)mouse - (Vector2)transform.position)
            .normalized;


        // RAYCAST À FRENTE DO PLAYER

        float raycastOffset = 0.6f;

        Vector2 raycastOrigin =
            (Vector2)transform.position
            + direction * raycastOffset;


        RaycastHit2D hit = Physics2D.Raycast(
            raycastOrigin,
            direction,
            interactionDistance,
            interactionLayers
        );


        // OBJETO DETETADO

        if (hit.collider != null)
        {
            GravityObject obj =
                hit.collider.GetComponent<GravityObject>();


            if (obj != null)
            {
                laser.enabled = true;


                // LINHA ONDULADA
                AnimateLaser(
                    gravityOrigin.position,
                    obj.transform.position
                );


                // COR DE HOVER
                SetLaserColor(hoverColor);


                // SELECIONAR COM E
                if (Input.GetKeyDown(KeyCode.E))
                {
                    selectedObject = obj;

                    SetLaserColor(selectedColor);
                }

                return;
            }
        }
        
        // NADA DETETADO

        laser.enabled = false;
    }


    void AnimateLaser(Vector3 start, Vector3 end)
    {
        int pointCount = 20;

        laser.positionCount = pointCount;

        Vector3 direction = end - start;

        Vector3 perpendicular =
            new Vector3(-direction.y, direction.x, 0f).normalized;

        for (int i = 0; i < pointCount; i++)
        {
            float t = (float)i / (pointCount - 1);

            Vector3 position =
                Vector3.Lerp(start, end, t);

            float wave =
                Mathf.Sin(
                    Time.time * laserWaveSpeed
                    + t * Mathf.PI * 4f
                )
                * laserWaveAmplitude;

            // Evita que as pontas do laser se afastem
            // do início e do fim
            float fade = Mathf.Sin(t * Mathf.PI);

            position += perpendicular * wave * fade;

            laser.SetPosition(i, position);
        }
    }


    void SetLaserColor(Color color)
    {
        Gradient gradient = new Gradient();

        gradient.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(color, 0f),
                new GradientColorKey(color, 1f)
            },

            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );

        laser.colorGradient = gradient;
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