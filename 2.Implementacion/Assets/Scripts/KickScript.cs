using UnityEngine;

public class KickScript : MonoBehaviour
{
    private Vector2 posicionInicial;
    private bool golpeando = false;
    [SerializeField] private float desplazamientoX = 0.5f;
    [SerializeField] private float desplazamientoY = 0.3f;
    [SerializeField] private float velocidadGolpe = 5f;
    [SerializeField] private float fuerzaGolpe = 10f; // Fuerza del golpe a la pelota

    void Start()
    {
        posicionInicial = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !golpeando)
        {
            golpeando = true;
            StartCoroutine(MoverPierna());
        }
    }

    private System.Collections.IEnumerator MoverPierna()
    {
        Vector2 objetivo = posicionInicial + new Vector2(desplazamientoX, desplazamientoY);

        while (Vector2.Distance(transform.localPosition, objetivo) > 0.01f)
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, objetivo, velocidadGolpe * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        while (Vector2.Distance(transform.localPosition, posicionInicial) > 0.01f)
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, posicionInicial, velocidadGolpe * Time.deltaTime);
            yield return null;
        }

        golpeando = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Balon"))
        {
            Rigidbody2D rbPelota = other.GetComponent<Rigidbody2D>();
            if (rbPelota != null)
            {
                Vector2 direccionGolpe = new Vector2(1, 1).normalized; // Dirección hacia adelante y arriba
                rbPelota.AddForce(direccionGolpe * fuerzaGolpe, ForceMode2D.Impulse);
            }
        }
    }
}
