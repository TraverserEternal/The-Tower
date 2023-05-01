using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGen : MonoBehaviour
{
  GameObject[] platforms = new GameObject[20];
  private void Start()
  {
    for (int i = 0; i < platforms.Length; i++)
    {
      platforms[i] = GeneratePlatform();
    }
  }

  private GameObject GeneratePlatform()
  {
    throw new NotImplementedException();
  }
}
