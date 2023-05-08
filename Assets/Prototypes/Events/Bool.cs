using UnityEngine;

namespace TowerEvent
{
  namespace WithState
  {
    [CreateAssetMenu(menuName = "Events/Stateful/Bool")]
    public class Bool : Stateful<bool> { };
  }
}