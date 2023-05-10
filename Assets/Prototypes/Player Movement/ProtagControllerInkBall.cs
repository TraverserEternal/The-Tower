using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtagControllerInkBall : MonoBehaviour
{
  [SerializeField][HideInInspector] ProtagController protagController;
  private void OnValidate()
  {
    protagController = GetComponentInParent<ProtagController>();
  }
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Ground")) protagController.ChangeToInkPool(other.gameObject);
  }
}
