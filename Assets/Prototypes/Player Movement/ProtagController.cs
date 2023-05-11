using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtagController : Singleton<ProtagController>
{
  #region Serialized Fields
  [SerializeField] float maxMoveSpeed = 7;
  [SerializeField] float acceleration = 30;
  [SerializeField] float stopAcceleration = 2f;
  [SerializeField] float minMoveSpeed = 2f;
  [SerializeField] public float maxOutOfPoolMoveSpeed = 15f;
  #endregion
  #region Autofilled Fields
  [SerializeField][HideInInspector] Rigidbody2D rb;
  [SerializeField][HideInInspector] GameObject humanoidObject;
  [SerializeField][HideInInspector] ProtagControllerHumanoid humanoid;
  [SerializeField][HideInInspector] GameObject inkPoolObject;
  [SerializeField][HideInInspector] ProtagControllerInkPool inkPool;
  [SerializeField][HideInInspector] GameObject inkBallObject;
  #endregion
  public enum PlayerForm
  {
    Humanoid,
    InkBall,
    InkPool
  }
  [HideInInspector] public TowerEvent.WithState<PlayerForm> currentForm { get; private set; }

  int moveDirection;
  int lastPressedMoveDirection;
  bool isAffecting = true;
  bool autoStop = true;
  protected override void Awake()
  {
    base.Awake();
    if (destroying) return;
    currentForm = ScriptableObject.CreateInstance<TowerEvent.PlayerForm>();
    ChangeToHumanoid();
  }
  #region Enable/Disable and Validation
  private void OnValidate()
  {
    rb = GetComponent<Rigidbody2D>();

    humanoid = GetComponentInChildren<ProtagControllerHumanoid>();
    humanoidObject = humanoid.gameObject;

    inkBallObject = GetComponentInChildren<ProtagControllerInkBall>().gameObject;

    inkPool = GetComponentInChildren<ProtagControllerInkPool>();
    inkPoolObject = inkPool.gameObject;
  }
  private void OnEnable()
  {
    I.actions.@base.move.performed += Move;
    I.actions.@base.move.canceled += Move;
    I.actions.@base.InkForm.performed += ChangeForm;
    I.actions.Enable();
  }
  private void OnDisable()
  {
    I.actions.@base.move.performed -= Move;
    I.actions.@base.move.canceled -= Move;
    I.actions.@base.InkForm.performed -= ChangeForm;
    I.actions.Disable();
  }
  #endregion
  #region Form Change Functions
  public void ChangeToHumanoid(bool shouldJump = false, bool removeVelocity = false)
  {
    humanoidObject.SetActive(true);
    inkPoolObject.SetActive(false);
    inkBallObject.SetActive(false);
    isAffecting = true;
    autoStop = true;
    currentForm.Set(PlayerForm.Humanoid);
    if (removeVelocity) rb.velocity = Vector2.zero;
    if (shouldJump) humanoid.ForceJump();
  }

  public void ChangeToInkBall()
  {
    inkBallObject.SetActive(true);
    inkPoolObject.SetActive(false);
    humanoidObject.SetActive(false);
    isAffecting = true;
    autoStop = false;
    currentForm.Set(PlayerForm.InkBall);
  }

  public void ChangeToInkPool(RaycastHit2D hit)
  {
    inkPoolObject.SetActive(true);
    inkBallObject.SetActive(false);
    humanoidObject.SetActive(false);
    currentForm.Set(PlayerForm.InkPool);
    isAffecting = false;

    if (moveDirection == 0)
    {
      if (rb.velocity.x > 0) { inkPool.Init(1, hit); return; }
      else if (rb.velocity.x == 0) { inkPool.Init(lastPressedMoveDirection, hit); return; }
      inkPool.Init(-1, hit);
      return;
    }
    inkPool.Init(moveDirection, hit);
  }
  #endregion

  private void ChangeForm(InputAction.CallbackContext context)
  {
    switch (currentForm.v)
    {
      case PlayerForm.Humanoid:
        var hit = GetGroundHit();
        if (hit)
        {
          ChangeToInkPool(hit);
          break;
        }
        if (rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        ChangeToInkBall();
        break;
      case PlayerForm.InkBall:
        ChangeToHumanoid();
        break;
      case PlayerForm.InkPool:
        transform.position += Vector3.up * 1;
        ChangeToHumanoid(false, true);
        break;
    }
  }
  private RaycastHit2D GetGroundHit()
  {
    int layerMask = LayerMask.GetMask("Ground");
    RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.down * .75f), Vector2.down, .1f, layerMask);
    return hit;
  }

  private void Move(InputAction.CallbackContext context)
  {
    moveDirection = (int)context.ReadValue<float>();
    if (context.performed) lastPressedMoveDirection = moveDirection;
    if (!(Math.Abs(rb.velocity.x) < minMoveSpeed)) return;
    rb.velocity = new Vector2(moveDirection * minMoveSpeed, rb.velocity.y);
  }

  private void Update()
  {
    if (!isAffecting) return;
    // Variable Setup
    var vX = rb.velocity.x;
    var vY = rb.velocity.y;
    var tAcceleration = acceleration * Time.deltaTime;

    // Stop or slow down the player if they don't want to be moving in the direction they're moving
    if ((autoStop && moveDirection == 0) || moveDirection * vX < 0)
    {
      if (Math.Abs(vX) < stopAcceleration) rb.velocity = new Vector2(0, vY);
      else rb.velocity += new Vector2(tAcceleration * (vX > 0 ? -1 : 1), 0);
      return;
    };

    // Add Acceleration with capped MoveSpeed
    if (Math.Abs(vX) < maxMoveSpeed) rb.velocity += new Vector2(tAcceleration * moveDirection, 0);
  }
}
