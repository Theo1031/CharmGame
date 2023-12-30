using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public float recoilForce = 10.0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public int maxAmmo = 6;
    public float reloadTime = 2f;
    public float shootCooldown = 0.5f;
    public float spreadAngle = 45f;

    private Rigidbody playerRigidbody;
    private int currentAmmo;
    private float lastShotTime;
    private bool isReloading = false;

    void Start()
    {
        playerRigidbody = GetComponentInParent<Rigidbody>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isReloading && Time.time - lastShotTime > shootCooldown)
        {
            Shoot();
            Recoil();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        if (currentAmmo >= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                float angleH = Random.Range(-spreadAngle / 2, spreadAngle / 2);
                float angleV = Random.Range(-spreadAngle / 2, spreadAngle / 2);
                Quaternion spreadRotation = Quaternion.Euler(angleV, angleH, 0);
                Quaternion bulletRotation = bulletSpawnPoint.rotation * spreadRotation;
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);
            }

            currentAmmo -= 5;
            lastShotTime = Time.time;
        }
    }

    void Recoil()
    {
        if (playerRigidbody != null)
        {
            float actualRecoilForce = recoilForce;
            if (IsGrounded())
            {
                actualRecoilForce *= 0.5f;
            }

            playerRigidbody.AddForce(-transform.forward * actualRecoilForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
