using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtagControllerInkPool : MonoBehaviour
{
  [SerializeField] float moveTime;
  [SerializeField] float moveSpeed;
  [HideInInspector] public int moveDirection;
  [SerializeField][HideInInspector] ProtagController protagController;
  [SerializeField][HideInInspector] Rigidbody2D rb;
  [SerializeField][HideInInspector] BoxCollider2D boxCollider;
  Collider2D groundCollider;
  GameObject ground;
  float timer;
  private void OnValidate()
  {
    protagController = GetComponentInParent<ProtagController>();
    rb = GetComponentInParent<Rigidbody2D>();
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

  private void ChangeBackToHuman(InputAction.CallbackContext context)
  {
    protagController.ChangeToHumanoid(true);
    rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
  }
  private void Update()
  {
    if (timer <= 0)
    {
      protagController.ChangeToHumanoid();
      return;
    }
    timer -= Time.deltaTime;

    // var hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, LayerMask.GetMask("Ground"));
    // rb.transform.position = new Vector3(hit.point.x, hit.point.y, 0);
  }

  internal void Init(GameObject ground, int moveDirection)
  {
    this.ground = ground;
    groundCollider = GetComponent<Collider2D>();
    this.moveDirection = moveDirection;

    var hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
    var cube = GameObject.CreatePrimitive(PrimitiveType.Quad);
    cube.transform.position = new Vector3(hit.point.x, hit.point.y, -1);
    rb.transform.position = new Vector3(hit.point.x, hit.point.y, 0);
    rb.simulated = false;
    timer = moveTime;
  }
}
