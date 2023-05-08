using UnityEngine;

namespace TowerEvent
{
  namespace WithState
  {
    [CreateAssetMenu(menuName = "Events/Stateful/Enums/SceneNameArray")]
    public class SceneNameArray : Stateful<SceneName[]> { };
  }
}