using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class ProtagControllerHumanoid : MonoBehaviour
{
  public TextMeshProUGUI textElement;
  public void UpdateTextElement()
  {
    // Build the string with property values
    string propertyValues = $"Jump Height: {jumpHeight}\n" +
                            $"Jump Buffer: {jumpBuffer}";

    // Set the text element's text to the property values
    textElement.text = propertyValues;
  }
  public void SetJumpHeight(float value)
  {
    jumpHeight = value;
  }

  public void SetJumpBuffer(float value)
  {
    jumpBuffer = value;
  }

  [SerializeField] public float jumpHeight = 1;
  [SerializeField] public float jumpBuffer = .1f;
  [SerializeField][HideInInspector] Rigidbody2D rb;
  [SerializeField][HideInInspector] BoxCollider2D boxCollider;
  bool jumping;
  float jumpBufferTimer;

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
    else jumpBufferTimer = jumpBuffer;
  }
  private bool IsGrounded()
  {
    int layerMask = LayerMask.GetMask("Ground");
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .8f, layerMask);
    return hit.collider != null;
  }
  private void Update()
  {
    UpdateTextElement();
    if (jumpBufferTimer > 0)
    {
      jumpBufferTimer -= Time.deltaTime;
      if (IsGrounded())
      {
        jumpBufferTimer = 0;
        ForceJump();
      }
    }
  }

  internal void ForceJump()
  {
    jumping = true;
    rb.AddForce(Vector2.up * Mathf.Sqrt(-2.0f * Physics2D.gravity.y * jumpHeight), ForceMode2D.Impulse);
  }
}
