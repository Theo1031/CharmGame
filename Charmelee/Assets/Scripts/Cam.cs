using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 2f;
    public float maxYAngle = 80f;

    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationY += mouseY;
        rotationY = Mathf.Clamp(rotationY, -maxYAngle, maxYAngle);

        transform.localRotation = Quaternion.Euler(-rotationY, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
