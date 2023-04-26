using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private Transform look;

    private Rigidbody playerRb;
    private float speed = 13.0f;
    private float gravity = 1.0f;

    private float jumpForce = 7.5f;
    private bool isGrounded = true;

    private int puCount = 0;

    private float puStrength = 10f;

    private void Awake(){
        playerRb = GetComponent<Rigidbody>();
    }

    [SerializeField]
    private GameObject puIndicator;

    void Start()
    {
        Debug.Log("objective: dodge balls lure them into goal, use powerups");
        Physics.gravity *= gravity;
        puIndicator.SetActive(puCount > 0);
    }

    void Update()
    {
        Move();
        Jump();
    }

    void LateUpdate()
    {
        puIndicator.transform.position = transform.position + new Vector3(0f,0.6f,0f);
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
        if(coll.gameObject.CompareTag("Enemy") && puCount > 0){
            // knockback enemies when power up
            Rigidbody enemyRb = coll.gameObject.GetComponent<Rigidbody>();
            Vector3 away = coll.gameObject.transform.position - transform.position;
            enemyRb.AddForce(away * puStrength, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Powerup")){
            // consume powerups and enable bool
            puCount ++;
            puIndicator.SetActive(puCount > 0);
            Destroy(other.gameObject);
            StartCoroutine(PowerupTimer());
        }
    }

    private IEnumerator PowerupTimer(){
        yield return new WaitForSeconds(10);
        puCount --;
        puIndicator.SetActive(puCount > 0);
    }
}
