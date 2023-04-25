using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private Transform look;

    private Rigidbody playerRb;
    private float speed = 3.0f;
    private float gravity = 1.0f;

    private float jumpForce = 5f;
    private bool isGrounded = true;

    private void Awake(){
        playerRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Physics.gravity *= gravity;
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void Move(){
        // user input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // calculate vector
        Vector3 dir = look.right * x + look.forward * z;

        dir *= speed;
        playerRb.AddForce(dir, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision coll){
        if(coll.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }
}
