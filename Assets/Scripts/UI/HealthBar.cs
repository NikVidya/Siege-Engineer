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
        // transform.localScale.Set(maxWidth * damageable.healthPercent, transform.localScale.y, transform.localScale.z);
        Debug.Log("made it");
        transform.localScale = new Vector3(maxWidth * damageable.healthPercent, transform.localScale.y, transform.localScale.z);
        spriteRenderer.color = gradient.Evaluate(damageable.healthPercent);
    }
}