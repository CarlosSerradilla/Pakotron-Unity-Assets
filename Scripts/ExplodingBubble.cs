using UnityEngine;

public class ExplodingBubble : MonoBehaviour
{
    public float explosionForce = 10f; // Fuerza de la explosión
    public float explosionRadius = 3f; // Radio de la explosión
    public GameObject explosionEffect; // Efecto visual de explosión (opcional)

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Player")) // Explota al tocar al jugador
        {*/
            Explode();
        /*}*/
    }

    void Explode()
    {
        // Instanciar efecto visual de explosión si está asignado
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Detectar objetos cercanos
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D obj in objectsInRange)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Aplicar una fuerza explosiva radial
                Vector2 direction = (rb.transform.position - transform.position).normalized;
                rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            }
        }

        // Destruir la burbuja después de la explosión
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Dibujar el área de explosión en la escena para depuración
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
