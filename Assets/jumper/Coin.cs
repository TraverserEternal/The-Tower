using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      JumperGameManager.c.numCoins.Set(JumperGameManager.c.numCoins.v + 1);
      Destroy(gameObject);
    }
  }
}
