using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    private bool puedeChutar = true;
    private bool enSuelo = false; // Evita saltos infinitos

    [SerializeField] float velocidad = 5f;
    [SerializeField] float fuerzaSalto = 5f;
    [SerializeField] Transform puntoGolpeo; // Un EmptyObject en la pierna
    [SerializeField]  private AudioSource KickSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // Movimiento lateral con linearVelocity
        float movimiento = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(movimiento * velocidad, rb.linearVelocity.y);

        // Salto con linearVelocity, solo si está en el suelo
        if (Input.GetKeyDown(KeyCode.UpArrow) && enSuelo)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            enSuelo = false; // Desactivar hasta que toque el suelo otra vez
        }

        // Animación de patada
        if (Input.GetKeyDown(KeyCode.Space) && puedeChutar)
        {
            anim.SetTrigger("Chutar"); // Activar el Trigger de la patada
            KickSound.Play(); // Reproducir el sonido de la patada
            puedeChutar = false; // Bloquear la patada hasta que se resetee
            Invoke("ResetPatear", 0.5f); // Resetear la capacidad de patear después de 0.5 segundos
        }
    }    


    void ResetPatear()
    {
        puedeChutar = true; // Restaurar la posibilidad de patear
    }

    // Detectar si toca el suelo
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SueloInvisible") || collision.gameObject.CompareTag("Iniesta") || collision.gameObject.CompareTag("Balon"))
        {
            enSuelo = true;
        }
    }

}