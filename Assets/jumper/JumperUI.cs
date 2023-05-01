using System;
using UnityEngine;
using UnityEngine.UIElements;

public class JumperUI : MonoBehaviour
{
  [SerializeField] UIDocument document;
  Label score;
  void Start()
  {
    JumperGameManager.c.numCoins.onChange += UpdateUI;
    score = document.rootVisualElement.Q<Label>("Score");
  }

  private void UpdateUI(int numCoins)
  {
    score.text = numCoins.ToString();
  }
}
