using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float slideSpeed = 10f;
    public int maxJumpCount = 2;
    public float maxVelocity = 10f;
    public int maxHealth = 100;


    private int jumpCount = 0;
    private bool isSliding = false;
    private Rigidbody rb;
    private int currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSliding)
        {
            Slide();
        }

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

        if (currentHealth <= 0)
        {
            QuitGame();
        }
    }

    void Jump()
    {
        jumpCount++;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Slide()
    {
        isSliding = true;
        StartCoroutine(SlideCoroutine());
    }

    IEnumerator SlideCoroutine()
    {
        float originalSpeed = speed;
        speed = slideSpeed;

        yield return new WaitForSeconds(4f);

        speed = originalSpeed;
        isSliding = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
}
