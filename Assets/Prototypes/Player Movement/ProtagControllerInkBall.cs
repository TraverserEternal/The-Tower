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
    if (other.gameObject.CompareTag("Ground") && CollisionIsAtTop(other)) protagController.ChangeToInkPool();
  }
  private bool CollisionIsAtTop(Collision2D collision)
  {
    // Calculate the contact points of the collision
    ContactPoint2D[] contacts = collision.contacts;

    // Iterate through the contact points
    for (int i = 0; i < contacts.Length; i++)
    {
      // Check if the contact normal is approximately pointing upwards (top)
      if (Vector2.Dot(contacts[i].normal, Vector2.up) > 0.8f)
      {
        return true;
      }
    }

    return false;
  }
}
