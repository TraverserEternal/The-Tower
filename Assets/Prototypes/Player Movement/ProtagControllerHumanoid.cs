using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ProtagControllerHumanoid : MonoBehaviour
{
  [SerializeField] float jumpHeight = 1;
  [SerializeField][HideInInspector] Rigidbody2D rb;
  [SerializeField][HideInInspector] BoxCollider2D boxCollider;
  bool jumping;

  private void OnValidate()
  {
    boxCollider = GetComponent<BoxCollider2D>();
    rb = GetComponentInParent<Rigidbody2D>();
  }

  // Start is called before the first frame update
  void OnEnable()
  {
    I.actions.@base.jump.performed += JumpPerformed;
    I.actions.@base.jump.canceled += JumpCanceled;
  }
  void OnDisable()
  {
    jumping = false;
    I.actions.@base.jump.performed -= JumpPerformed;
    I.actions.@base.jump.canceled -= JumpCanceled;
  }

  private void JumpCanceled(InputAction.CallbackContext context)
  {
    if (jumping && !IsGrounded() && rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, 0, 2));
    else jumping = false;
  }

  private void JumpPerformed(InputAction.CallbackContext context)
  {
    if (IsGrounded()) ForceJump();
  }
  private bool IsGrounded()
  {
    int layerMask = LayerMask.GetMask("Ground");
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .8f, layerMask);
    return hit.collider != null;
  }

  internal void ForceJump()
  {
    jumping = true;
    rb.AddForce(Vector2.up * Mathf.Sqrt(-2.0f * Physics2D.gravity.y * jumpHeight), ForceMode2D.Impulse);
  }
}
