using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ProtagControllerHumanoid : MonoBehaviour
{
  [SerializeField] float jumpForce = 1;
  [SerializeField] float jumpHoldTime;
  [SerializeField][HideInInspector] Rigidbody2D rb;
  [SerializeField][HideInInspector] BoxCollider2D boxCollider;
  Coroutine jump;
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
    I.actions.@base.jump.performed -= JumpPerformed;
    I.actions.@base.jump.canceled -= JumpCanceled;
  }

  private void JumpCanceled(InputAction.CallbackContext context)
  {
    if (jump != null)
    {
      StopCoroutine(jump);
      if (rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, 0);
      jump = null;
    }

  }

  private void JumpPerformed(InputAction.CallbackContext context)
  {
    StartJump();
  }

  public void StartJump()
  {
    jump = StartCoroutine(Jump());
  }


  private bool IsGrounded()
  {
    int layerMask = LayerMask.GetMask("Ground");
    RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.down * (boxCollider.bounds.size.y / 2)), Vector2.down, .1f, layerMask);
    return hit.collider != null;
  }

  private IEnumerator Jump()
  {
    if (!IsGrounded()) yield break;
    float timer = jumpHoldTime;
    while (timer > 0)
    {
      rb.velocity = new Vector2(rb.velocity.x, jumpForce * timer / jumpHoldTime);
      timer -= Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }
    jump = null;
  }

}
