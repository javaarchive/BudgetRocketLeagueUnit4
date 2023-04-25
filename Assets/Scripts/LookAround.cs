using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{

    private float sensitivity = 20f;

    [SerializeField]
    private Transform player;

    private float newX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");

        // left right
        player.eulerAngles += Vector3.up * y * sensitivity;
        transform.eulerAngles += Vector3.up * y * sensitivity;

        // up and down
        newX += x * sensitivity * -1f;
        newX = Mathf.Clamp(newX, -25f, 50f);
        transform.eulerAngles = new Vector3(newX, transform.eulerAngles.y, transform.eulerAngles.z);
    
        // follow player
        transform.position = player.position;
    }
}
