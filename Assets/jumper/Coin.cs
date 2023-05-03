using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Coin : MonoBehaviour
{

  [SerializeField] private GameObject text;

  public void Interact()
  {
      Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.CompareTag("Player")) 
    {
      text.SetActive(true);
    }
  }
  private void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.CompareTag("Player")) 
    {
      text.SetActive(false);
    }
  }
}
