using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        Destroy(gameObject, 3f);
    }
}
