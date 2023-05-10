using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtagControllerInkPool : MonoBehaviour
{
  #region Serialized Fields
  [SerializeField] float moveTime;
  [SerializeField] float moveSpeed;
  #endregion
  #region Autofilled Fields;
  [SerializeField][HideInInspector] ProtagController protagController;
  [SerializeField][HideInInspector] BoxCollider2D humanoidCollider;
  [SerializeField][HideInInspector] Rigidbody2D rb;
  [SerializeField][HideInInspector] BoxCollider2D boxCollider;
  #endregion
  [HideInInspector] public int moveDirection;
  Collider2D groundCollider;
  GameObject ground;
  float timer;
  #region Enable/Disable, Validation
  private void OnValidate()
  {
    protagController = GetComponentInParent<ProtagController>();
    rb = GetComponentInParent<Rigidbody2D>();
    humanoidCollider = rb.GetComponentInChildren<ProtagControllerHumanoid>().GetComponent<BoxCollider2D>();
    boxCollider = GetComponentInParent<BoxCollider2D>();
  }
  private void OnEnable()
  {
    I.actions.@base.jump.performed += ChangeBackToHuman;
  }
  private void OnDisable()
  {
    I.actions.@base.jump.performed -= ChangeBackToHuman;
    rb.simulated = true;
  }
  #endregion

  private void ChangeBackToHuman(InputAction.CallbackContext context)
  {
    rb.gameObject.transform.position += Vector3.up * 1;
    rb.velocity = new Vector2(moveSpeed * moveDirection, 0);
    protagController.ChangeToHumanoid(true, false);
  }
  private void Update()
  {
    if (timer <= 0)
    {
      rb.gameObject.transform.position += Vector3.up * 1;
      rb.velocity = new Vector2(Math.Clamp(rb.velocity.x, -protagController.maxOutOfPoolMoveSpeed, protagController.maxOutOfPoolMoveSpeed), 0);
      protagController.ChangeToHumanoid(false, true);
      return;
    }
    timer -= Time.deltaTime;

    var hit = Physics2D.Raycast(transform.position, Vector2.up, .5f, LayerMask.GetMask("Ground"));
    var leftmostPosition = hit.transform.position.x - hit.collider.bounds.extents.x + boxCollider.bounds.extents.x;
    var rightmostPosition = hit.transform.position.x + hit.collider.bounds.extents.x - boxCollider.bounds.extents.x;
    float nextPositionx = Math.Clamp(hit.point.x + moveSpeed * moveDirection * Time.deltaTime, leftmostPosition, rightmostPosition);
    rb.transform.position = new Vector3(
      nextPositionx,
      hit.point.y - boxCollider.bounds.extents.y,
      0);
  }

  internal void Init(int moveDirection, RaycastHit2D hit)
  {
    this.moveDirection = moveDirection;

    rb.transform.position = new Vector3(hit.point.x, hit.point.y - boxCollider.bounds.extents.y, 0);
    rb.simulated = false;
    timer = moveTime;
  }
}
