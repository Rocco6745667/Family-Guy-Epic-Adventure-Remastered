using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 10f;

    private Vector2 direction;

    void Start()
    {
        direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        Destroy(gameObject, 5f); // Destroy projectile after 5 seconds
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy projectile on impact
        }
    }
}
