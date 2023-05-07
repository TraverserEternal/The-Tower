using System;
using TowerEvent;
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
  public override Stateful<bool> GetSaveData() => HearthanSaveManager.current.data.doorOpened;

  protected override void OnSaveDataChange(bool doorOpened)
  {
    boxCollider2D.enabled = !doorOpened;
    spriteRenderer.enabled = !doorOpened;
  }
}
