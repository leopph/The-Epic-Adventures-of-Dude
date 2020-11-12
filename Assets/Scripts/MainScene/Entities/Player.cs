using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]


public class Player : Entity
{
    struct DashData
    {
        public enum DashState
        {
            Dashing, Ready, Cooldown
        }

        public DashState state;
        public float originX;
        public float targetX;
        public float t;
        public float cooldown;
    }


    private Rigidbody2D m_Body;
    private BoxCollider2D m_Collider;

    private ContactPoint2D[] m_ContactPoints = new ContactPoint2D[2];

    private DashData m_DashData = new DashData();

    public float m_JumpForce = 1.0f;
    private byte m_JumpCount = 0;
    private bool m_Jumping = false;

    private float m_Move = 0f;
    public float m_MoveSpeed = 1.0f;




    protected override void Awake()
    {
        base.Awake();

        m_MaxHealth = 100f;
        m_Health = m_MaxHealth;
        m_IsFacingRight = false;

        m_Body = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<BoxCollider2D>();
        m_Animator = GetComponentInChildren<Animator>();
    }



    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
    }



    private void FixedUpdate()
    {
        m_Body.velocity = new Vector2(m_Move * m_MoveSpeed, m_Body.velocity.y);

        if (m_Jumping)
        {
            Jump();
            m_Jumping = false;
        }

        if ((Camera.main.WorldToScreenPoint(transform.position).x < Input.mousePosition.x && !m_IsFacingRight) || (Camera.main.WorldToScreenPoint(transform.position).x > Input.mousePosition.x && m_IsFacingRight))
            Flip();

        if (m_DashData.state == DashData.DashState.Cooldown)
            if (m_DashData.cooldown > 0f)
                m_DashData.cooldown -= Time.deltaTime;

            else
                m_DashData.state = DashData.DashState.Ready;
        else if (m_DashData.state == DashData.DashState.Dashing)
        {
            m_DashData.t += Time.deltaTime / 0.2f;

            transform.position = Vector3.Lerp(new Vector3(m_DashData.originX, transform.position.y, transform.position.z), new Vector3(m_DashData.targetX, transform.position.y, transform.position.z), m_DashData.t);
            m_Body.velocity = Vector2.zero;

            if (transform.position.x == m_DashData.targetX)
            {
                m_DashData.state = DashData.DashState.Cooldown;
                m_Animator.SetBool("Dashing", false);
            }
        }
    }



    void Update()
    {
        if (m_Health < 0f)
        {
            Die();
            return;
        }

        m_Move = Input.GetAxis("Horizontal");
        m_Animator.SetFloat("Velocity[x]", Mathf.Abs(m_Body.velocity.x));

        if (Input.GetButtonDown("Jump") && m_JumpCount < 2)
            m_Jumping = true;
        m_Animator.SetBool("Jumping", m_JumpCount > 0);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            if (m_DashData.state == DashData.DashState.Ready)
            {
                m_DashData.state = DashData.DashState.Dashing;
                m_DashData.originX = transform.position.x;
                m_DashData.targetX = transform.position.x + 5f * (m_IsFacingRight ? 1f : -1f);
                m_DashData.t = 0f;
                m_DashData.cooldown = 2f;

                m_Animator.SetBool("Dashing", true);
            }
            else if (m_DashData.state == DashData.DashState.Cooldown)
                Debug.Log("Cooldown: " + Mathf.RoundToInt(m_DashData.cooldown));
    }



    private void OnCollisionEnter2D()
    {
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
        if (m_DashData.state == DashData.DashState.Dashing)
        {
            if (collision.contactCount > m_ContactPoints.Length)
                m_ContactPoints = new ContactPoint2D[(int)Mathf.Pow(2, m_ContactPoints.Length)];

            collision.GetContacts(m_ContactPoints);

            foreach (ContactPoint2D contactPoint in m_ContactPoints)
                if (contactPoint.point.y >= collision.otherCollider.bounds.min.y)
                {
                    m_DashData.state = DashData.DashState.Cooldown;
                    m_Animator.SetBool("Dashing", false);
                    break;
                }

        }
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



    public void Die()
    {
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
        transform.localScale = Vector3.Reflect(transform.localScale, Vector3.left);
    }
}