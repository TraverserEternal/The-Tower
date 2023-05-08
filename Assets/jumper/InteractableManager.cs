using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager
{
  private GameObject player;
  private List<Coin> potentialInteractables;
  public void Interact()
  {
    Coin closestCoin = null;
    float distanceFromClosest = float.PositiveInfinity;
    potentialInteractables.ForEach(coin =>
    {
      if (closestCoin == null || distanceFromClosest > Vector2.Distance(player.transform.position, coin.transform.position))
      {
        closestCoin = coin;
        distanceFromClosest = Vector2.Distance(player.transform.position, coin.transform.position);
      }
    });
    closestCoin.Interact();
  }
}
