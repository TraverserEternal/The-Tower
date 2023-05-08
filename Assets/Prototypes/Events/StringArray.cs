using UnityEngine;

namespace TowerEvent
{
  namespace WithState
  {
    [CreateAssetMenu(menuName = "Events/Stateful/StringArray")]
    public class StringArray : Stateful<string[]> { };
  }
}