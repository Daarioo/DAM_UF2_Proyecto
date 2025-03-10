using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float factorRebote = 1.3f; 
    private float groundFriction = 0.98f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.1f) // Si casi no hay rebote
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * groundFriction, rb.linearVelocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Obtener la normal de la colisión
        Vector2 normal = collision.contacts[0].normal;

        // Reflejar la velocidad usando la normal de la colisión
        rb.linearVelocity = Vector2.Reflect(rb.linearVelocity, normal) * factorRebote;
    }
}

