using UnityEngine;

public class Door : MonoBehaviour
{
  private void Start()
  {
    HearthanSaveManager.current.data.doorUnlocked.Subscribe(SetDoor);
  }

  private void SetDoor(bool doorUnlocked)
  {
    if (doorUnlocked) gameObject.SetActive(false);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    HearthanSaveManager.current.data.doorUnlocked.Set(true);
    gameObject.SetActive(false);
  }
}
