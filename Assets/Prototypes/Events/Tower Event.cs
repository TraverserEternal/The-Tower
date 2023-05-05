public partial class TowerEvent
{
  public System.Action onTrigger;
  public virtual void Trigger()
  {
    if (onTrigger != null) onTrigger();
  }
}