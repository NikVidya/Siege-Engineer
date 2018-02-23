using UnityEngine;

// TODO make coloour of health bar change based on health (gradient)
public class HealthBar : MonoBehaviour
{
    // public
    public Damageable damageable;
    [SerializeField]
    private Gradient gradient;
    // private
    private float maxWidth;
    private SpriteRenderer spriteRenderer;
    private Camera camera;
    private Vector2 screenPos;
    private bool movingWithCamera = false;
    private Vector2 defaultPos;
    void Start()
    {
        maxWidth = transform.localScale.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
        camera = Camera.main;
        defaultPos = new Vector2(transform.position.x, transform.position.y);

    }
    void Update()
    {
        screenPos = camera.WorldToScreenPoint(transform.position);
        if ((screenPos.y > Screen.height || screenPos.y < 0 || screenPos.x > Screen.width || screenPos.x < 0) && !movingWithCamera)
        {
            MoveToCamera();
        }
        else
        {
            MoveToDefault();
        }
        if (movingWithCamera)
        {
        }
    }
    public void changeWidth()
    {
        transform.localScale = new Vector3(maxWidth * damageable.healthPercent, transform.localScale.y, transform.localScale.z);
        spriteRenderer.color = gradient.Evaluate(damageable.healthPercent);
    }

    // Attach the healthbar to the camera
    void MoveToCamera()
    {
        movingWithCamera = true;
        transform.SetParent(camera.transform);
    }
    void MoveToDefault()
    {
        movingWithCamera = false;
        transform.position = defaultPos;
    }
}