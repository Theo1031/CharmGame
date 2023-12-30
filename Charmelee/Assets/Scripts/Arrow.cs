using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifeTime = 3f; // Time before the arrow gets destroyed

void Start()
{
    Destroy(gameObject, lifeTime);
}

public void Launch(Vector3 initialVelocity)
{
    Rigidbody rb = GetComponent<Rigidbody>();
    rb.velocity = initialVelocity;
}

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(1);
        }
    }
}
}