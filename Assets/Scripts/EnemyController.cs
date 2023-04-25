using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private float speed = 10.0f;
    private Rigidbody enemyRb;

    [SerializeField]
    private GameObject player;

    private void Awake(){
        enemyRb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("PlayerSphere");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 look = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(look * speed);
    }

    private void OnCollisionEnter(Collision coll){
        if(coll.gameObject.CompareTag("Goal")){
            Destroy(gameObject);
        }
    }
}
