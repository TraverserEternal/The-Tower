using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D eRb;
    [SerializeField] float enemySpeed = 1;
    [SerializeField] float enemyJumpForce = 1; // how do i make it jump!!!! :(
    float timer;
    [SerializeField] float timeBtwJump;
    Transform target;
    Vector2 moveDirection;

    private void Awake()
    {
        //initilizes the rigid body 
        eRb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)// checks that target is not null
        {
            moveDirection = (target.position - transform.position).normalized; // gets the location of where to go
        }
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
    }
    private void FixedUpdate() //called more frequently than update 
    {
        if (target)
        {

            eRb.velocity = new Vector2(moveDirection.x * enemySpeed, eRb.velocity.y);
        }

        if (moveDirection.y > 0 && timer <= 0)
        {
            timer = timeBtwJump;
            eRb.AddForce(Vector2.up * enemyJumpForce, ForceMode2D.Impulse);
            Debug.Log("its working");

        }
    }
}
