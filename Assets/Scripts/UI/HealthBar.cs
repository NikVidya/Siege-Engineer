using UnityEngine;

// TODO add outline/background to health bar to indicate not only max health but also damage rate
public class HealthBar : MonoBehaviour
{
    // public
    public Damageable damageable;
    [SerializeField]
    private Gradient gradient;
    // private
    private float maxWidth;
    private SpriteRenderer spriteRenderer;
    private Vector2 screenPos;
    private bool movingWithCamera = false;
    private Vector2 defaultPos;
    void Start()
    {
        maxWidth = transform.localScale.x;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void Update()
    {

    }
    public void changeWidth()
    {
        transform.localScale = new Vector3(maxWidth * damageable.healthPercent, transform.localScale.y, transform.localScale.z);
        spriteRenderer.color = gradient.Evaluate(damageable.healthPercent);
    }

}