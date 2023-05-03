using UnityEngine;

public class TestController : Singleton<TestController>
{
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private float jumpForce = 10f;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private float groundRadius = 0.2f;
  [SerializeField] private LayerMask groundLayer;

  private Rigidbody2D rb;
  private bool isGrounded;

  protected override void Awake()
  {
    base.Awake();
    rb = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate()
  {
    float horizontalInput = Input.GetAxis("Horizontal");
    rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
  }

  private void Update()
  {
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

    if (isGrounded && Input.GetKeyDown(KeyCode.Space))
    {
      rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
  }
}