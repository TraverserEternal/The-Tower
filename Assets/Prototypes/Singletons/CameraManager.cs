using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
  [SerializeField][HideInInspector] public CinemachineBrain brain { get; private set; }
  private void OnValidate()
  {
    brain = GetComponent<CinemachineBrain>();
  }

}
