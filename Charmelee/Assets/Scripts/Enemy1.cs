using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float enemy1Speed = 3f;
    public int damage = 1;

    public ParticleSystem deathParticles; 
    public GameObject DeathDrop;

    private Transform player;
    private Animator animator; // Add this line

    void Start()
    {
        // Find the player object by tag and get its transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found");
        }

        // Get the Animator component
        animator = GetComponent<Animator>(); // Add this line
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards the player
            GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, player.position, enemy1Speed * Time.deltaTime));

            // Rotate to face the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flat rotation on the y axis
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Play the Move animation
            animator.SetBool("Anim_Goblin_Warrior_Move", true); // Add this line
        }
        else
        {
            // Play the Idle animation
            animator.SetBool("Anim_Goblin_Warrior_Idle", false); // Add this line
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                // Play the Damage animation
                animator.SetTrigger("Damage"); // Add this line
            }
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy the bullet and the enemy
            Destroy(collision.gameObject);
            Destroy(gameObject);

            // Play the Die animation
            animator.SetTrigger("Die"); // Add this line

            // Instantiate death particles and drop item if they are set
            if (deathParticles != null)
            {
                Instantiate(deathParticles, transform.position, Quaternion.identity);
            }

            if (DeathDrop != null)
            {
                //Instantiate(DeathDrop, transform.position, Quaternion.identity);
            }
        }
    }
}