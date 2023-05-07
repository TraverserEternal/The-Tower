using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemySpeed = 1;
    [SerializeField] float enemyJumpForce = 1;
    Transform target;
    [SerializeField] Rigidbody2D eRb;
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
            Vector3 direction = (target.position-transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate() 
    {
        if (target)
        {
            eRb.velocity = new Vector2(moveDirection.x,moveDirection.y)*enemySpeed;
        }
    }
}
