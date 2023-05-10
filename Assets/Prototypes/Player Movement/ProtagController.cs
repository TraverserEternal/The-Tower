using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtagController : MonoBehaviour
{
  [SerializeField] float maxMoveSpeed = 7;
  [SerializeField] float acceleration = 30;
  [SerializeField] float stopAcceleration = 2f;
  [SerializeField] float minMoveSpeed = 2f;
  [SerializeField][HideInInspector] Rigidbody2D rb;
  [SerializeField][HideInInspector] GameObject humanoidObject;
  [SerializeField][HideInInspector] ProtagControllerHumanoid humanoid;
  [SerializeField][HideInInspector] GameObject inkPoolObject;
  [SerializeField][HideInInspector] ProtagControllerInkPool inkPool;
  [SerializeField][HideInInspector] GameObject inkBallObject;
  public enum PlayerForm
  {
    Humanoid,
    InkBall,
    InkPool
  }
  [HideInInspector] public TowerEvent.WithState<PlayerForm> currentForm { get; private set; }

  int moveDirection;
  bool isAffecting = true;
  protected void Awake()
  {
    // base.Awake();
    // if (destroying) return;
    currentForm = ScriptableObject.CreateInstance<TowerEvent.PlayerForm>();
    ChangeToHumanoid();
  }
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

  public void ChangeToHumanoid(bool shouldJump = false)
  {
    humanoidObject.SetActive(true);
    inkPoolObject.SetActive(false);
    inkBallObject.SetActive(false);
    isAffecting = true;
    currentForm.Set(PlayerForm.Humanoid);
    if (shouldJump) humanoid.StartJump();
  }

  public void ChangeToInkBall()
  {
    inkBallObject.SetActive(true);
    inkPoolObject.SetActive(false);
    humanoidObject.SetActive(false);
    isAffecting = true;
    currentForm.Set(PlayerForm.InkBall);
  }

  public void ChangeToInkPool(GameObject ground)
  {
    inkBallObject.SetActive(false);
    humanoidObject.SetActive(false);
    currentForm.Set(PlayerForm.InkPool);
    isAffecting = false;

    int lockedMoveDirection;
    if (moveDirection == 0)
    {
      if (rb.velocity.x > 0) lockedMoveDirection = 1;
      else lockedMoveDirection = -1;
      return;
    }
    lockedMoveDirection = moveDirection;

    inkPoolObject.SetActive(true);
    inkPool.Init(ground, lockedMoveDirection);
  }

  private void ChangeForm(InputAction.CallbackContext context)
  {
    switch (currentForm.v)
    {
      case PlayerForm.Humanoid:
        ChangeToInkBall();
        break;
      case PlayerForm.InkBall:
        ChangeToHumanoid();
        break;
    }
  }


  private void Move(InputAction.CallbackContext context)
  {
    // get the value of the move action's axis
    moveDirection = (int)context.ReadValue<float>();
    if (Math.Abs(rb.velocity.x) < .2) rb.velocity = new Vector2(moveDirection * minMoveSpeed, rb.velocity.y);
  }

  private void Update()
  {
    if (!isAffecting) return;
    // Variable Setup
    var vX = rb.velocity.x;
    var vY = rb.velocity.y;
    var tAcceleration = acceleration * Time.deltaTime;

    // Stop or slow down the player if they don't want to be moving in the direction they're moving
    if ((moveDirection == 0 || moveDirection * vX < 0))
    {
      if (Math.Abs(vX) < stopAcceleration) rb.velocity = new Vector2(0, vY);
      else rb.velocity += new Vector2(tAcceleration * (vX > 0 ? -1 : 1), 0);
      return;
    };

    // Add Acceleration with capped MoveSpeed
    if (Math.Abs(vX) < maxMoveSpeed) rb.velocity += new Vector2(tAcceleration * moveDirection, 0);
  }
}
