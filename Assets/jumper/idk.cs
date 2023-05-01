using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class idk : MonoBehaviour
{
    private float moveDirection = 0;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float jumpForce = 1;
    // Start is called before the first frame update
    void Start()
    {
        I.actions.@base.jump.performed += Jump;
        I.actions.@base.move.performed += Move;
        I.actions.@base.move.canceled += Move;
        I.actions.Enable();
    }

    private void Update() 
    {
        // move the player left or right based on the axis value
        rb.AddForce(Vector3.right * moveDirection * moveSpeed * Time.deltaTime);
        if (moveDirection == 0) rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("we jumped!");
    }

    private void Move(InputAction.CallbackContext context)
    {
        // get the value of the move action's axis
        moveDirection = context.ReadValue<float>();
        Debug.Log("move changed to " + moveDirection);
    }
}
