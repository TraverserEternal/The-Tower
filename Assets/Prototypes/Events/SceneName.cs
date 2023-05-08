using UnityEngine;

namespace TowerEvent
{
  namespace WithState
  {
    [CreateAssetMenu(menuName = "Events/Stateful/Enums/SceneName")]
    public class SceneName : Stateful<SceneNames> { };
  }
}