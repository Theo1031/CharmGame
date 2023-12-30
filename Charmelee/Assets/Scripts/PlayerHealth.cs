using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float damageCooldown = 1f;

    private int currentHealth;
    private float lastDamageTime;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            currentHealth -= damage;
            lastDamageTime = Time.time;

            if (currentHealth <= 0)
            {
                SceneManager.LoadScene("End");
            }
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
