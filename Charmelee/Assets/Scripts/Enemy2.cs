using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public ParticleSystem deathParticles; 
    public float speed = 5f;
    public float shootingInterval = 2f;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;

    private Transform player;
    private float nextShootTime;
    private Animator animator;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found");
        }

        nextShootTime = Time.time + shootingInterval;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > 10f)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            if (Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootingInterval;
            }
        }
    }

    void Shoot()
    {
        Vector3 directionToPlayer = (player.position - arrowSpawnPoint.position).normalized;
        float distanceToPlayer = Vector3.Distance(arrowSpawnPoint.position, player.position);
        Vector3 initialVelocity = CalculateLaunchVelocity(distanceToPlayer, directionToPlayer);
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation);
        arrow.transform.Rotate(0, -25, 0);
        arrow.GetComponent<Arrow>().Launch(initialVelocity);
        animator.SetTrigger("Shoot");
    }

    Vector3 CalculateLaunchVelocity(float distance, Vector3 direction)
    {
        float launchAngle = 45f;
        float gravity = 9.81f;
        float velocityMagnitude = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * launchAngle * Mathf.Deg2Rad));
        Vector3 velocity = velocityMagnitude * direction.normalized;
        velocity.y = velocityMagnitude * Mathf.Sin(launchAngle * Mathf.Deg2Rad);
        return velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            animator.SetTrigger("Die");

            if (deathParticles != null)
            {
                Instantiate(deathParticles, transform.position, Quaternion.identity);
            }
        }
    }
}