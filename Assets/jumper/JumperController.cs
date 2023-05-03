using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class idk : MonoBehaviour
{
  private float moveDirection = 0;
  public InteractableManager interactableManager {get; private set;} = new InteractableManager();
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
    rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
  }

  private void Jump(InputAction.CallbackContext context)
  {
    if (IsGrounded()) rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
  }

  private bool IsGrounded()
  {
    int layerMask = ~(1 << LayerMask.NameToLayer("Player"));
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, layerMask);
    return hit.collider != null;
  }

  private void Move(InputAction.CallbackContext context)
  {
    // get the value of the move action's axis
    moveDirection = context.ReadValue<float>();
  }
}
