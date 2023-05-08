using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Door : SaveListener<bool>
{
  private BoxCollider2D boxCollider2D;
  private SpriteRenderer spriteRenderer;
  private void OnValidate()
  {
    boxCollider2D = GetComponent<BoxCollider2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }
  public override TowerEvent.WithState<bool> GetSaveData() => HearthanSaveManager.current.data.doorOpened;

  protected override void OnSaveDataChange(bool doorOpened)
  {
    boxCollider2D.enabled = !doorOpened;
    spriteRenderer.enabled = !doorOpened;
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("triggered!");
    if (other.gameObject.CompareTag("Player")) GetSaveData().Set(true);
  }
}
