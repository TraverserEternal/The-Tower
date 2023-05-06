using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerEvent
{
  namespace WithState
  {
    [CreateAssetMenu(menuName = "Events/Stateful/String")]
    public class String : Stateful<string> { };
  }
}