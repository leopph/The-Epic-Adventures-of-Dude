using UnityEngine;




public class Player : Entity
{
    private enum DashState
    {
        Ready, Dashing, Cooldown
    }

    private DashState m_DashState;
    private Vector3 m_DashOrigin;
    private Vector3 m_DashTarget;
    private float m_DashInterpolationPoint;
    private float m_DashCooldown;
    private const float m_DashTime = 0.1f;
    private readonly Vector3 m_DashDeltaPosition = new Vector3(5f, 0, 0);

    private Rigidbody2D m_Body;
    private BoxCollider2D m_Collider;

    private ContactPoint2D[] m_ContactPoints = new ContactPoint2D[2];

    public float m_JumpForce = 1.0f;
    private byte m_JumpCount = 0;
    private bool m_Jump = false;

    private float m_Move = 0f;
    public float m_MoveSpeed = 1.0f;

    private Transform m_BodyObject;



    private void Awake()
    {
        m_MaxHealth = 100f;
        m_Health = m_MaxHealth;
        m_IsFacingRight = false;
    }



    protected override void Start()
    {
        base.Start();

        m_Body = GetComponentInChildren<Rigidbody2D>();
        m_Collider = GetComponentInChildren<BoxCollider2D>();

        m_BodyObject = transform.GetChild(1);

        m_Animator = GetComponentInChildren<Animator>();
        m_AudioManager.Play("Ambient");

        m_HealthBar = GetComponentInChildren<HealthBar>();
        m_HealthBar.Init(100);
    }



    private void FixedUpdate()
    {
        if (m_DashState != DashState.Dashing)
        {
            m_Body.velocity = new Vector2(m_Move * m_MoveSpeed, m_Body.velocity.y);

            if (m_Jump)
            {
                Jump();
                m_Jump = false;
            }

            if ((Camera.main.WorldToScreenPoint(transform.position).x < Input.mousePosition.x && !m_IsFacingRight) || (Camera.main.WorldToScreenPoint(transform.position).x > Input.mousePosition.x && m_IsFacingRight))
                Flip();
        }
    }



    private void Update()
    {
        m_Move = Input.GetAxisRaw("Horizontal");
        m_Animator.SetFloat("Velocity[x]", Mathf.Abs(m_Body.velocity.x));

        if (Input.GetButtonDown("Jump") && m_JumpCount < 2)
            m_Jump = true;
        m_Animator.SetBool("Jumping", m_JumpCount > 0);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (m_DashState == DashState.Ready)
            {
                m_DashState = DashState.Dashing;
                m_DashOrigin = transform.position;
                m_DashTarget = transform.position + (m_IsFacingRight ? m_DashDeltaPosition : Vector3.Reflect(m_DashDeltaPosition, Vector3.right));
                m_DashInterpolationPoint = 0f;
                m_DashCooldown = 2f;
                m_Body.velocity = Vector2.zero;
                m_Animator.SetBool("Dashing", true);
                m_AudioManager.Play("Dash");
            }

            else if (m_DashState == DashState.Cooldown)
                Debug.Log("Cooldown: " + Mathf.RoundToInt(m_DashCooldown));
        }

        if (m_DashState == DashState.Cooldown)
        {
            if (m_DashCooldown > 0f)
                m_DashCooldown -= Time.deltaTime;

            else
                m_DashState = DashState.Ready;
        }

        else if (m_DashState == DashState.Dashing)
        {
            m_DashInterpolationPoint += Time.deltaTime / m_DashTime;
            transform.position = Vector3.Lerp(m_DashOrigin, m_DashTarget, m_DashInterpolationPoint);

            if (transform.position == m_DashTarget)
            {
                m_DashState = DashState.Cooldown;
                m_Animator.SetBool("Dashing", false);
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        DashCollisionTest(collision);

        if (m_JumpCount > 0)
        {
            float extraHeightTest = 0.05f;
            float testBoxDistance = extraHeightTest / 2f + 0.01f;
            Vector2 bottomCenter = new Vector2(m_Collider.bounds.center.x, m_Collider.bounds.min.y);

            if (Physics2D.BoxCast(bottomCenter - new Vector2(0, testBoxDistance), new Vector2(m_Collider.bounds.size.x, extraHeightTest), 0f, Vector2.down, 0f))
                m_JumpCount = 0;
        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        DashCollisionTest(collision);
    }



    private void OnCollisionExit2D()
    {
        if (m_JumpCount == 0)
        {
            float extraHeightTest = 0.05f;
            float testBoxDistance = extraHeightTest / 2f + 0.01f;
            Vector2 bottomCenter = new Vector2(m_Collider.bounds.center.x, m_Collider.bounds.min.y);

            if (!Physics2D.BoxCast(bottomCenter - new Vector2(0, testBoxDistance), new Vector2(m_Collider.bounds.size.x, extraHeightTest), 0f, Vector2.down, 0f))
                m_JumpCount++;
        }
    }



    public override void Die()
    {
        m_Animator.SetBool("Dashing", false);
        m_DashState = DashState.Ready;

        m_CheckpointManager.ReloadCheckPoint();
    }



    private void Jump()
    {
        if (m_JumpCount != 0)
            m_Body.velocity = new Vector2(m_Body.velocity.x, 0);

        m_Body.AddForce(new Vector2(0, m_JumpForce), ForceMode2D.Impulse);

        m_JumpCount++;
    }



    private void Flip()
    {
        m_IsFacingRight = !m_IsFacingRight;
        //transform.localScale = Vector3.Reflect(transform.localScale, Vector3.left);
        m_BodyObject.localScale = Vector3.Reflect(m_BodyObject.localScale, Vector3.left);
    }



    private void DashCollisionTest(Collision2D collision)
    {
        if (m_DashState == DashState.Dashing)
        {
            if (collision.contactCount > m_ContactPoints.Length)
                m_ContactPoints = new ContactPoint2D[(int)Mathf.Pow(2, m_ContactPoints.Length)];

            collision.GetContacts(m_ContactPoints);

            foreach (ContactPoint2D contactPoint in m_ContactPoints)
                if (contactPoint.point.y >= collision.otherCollider.bounds.min.y)
                {
                    m_DashState = DashState.Cooldown;
                    m_Animator.SetBool("Dashing", false);
                    break;
                }
        }
    }
}