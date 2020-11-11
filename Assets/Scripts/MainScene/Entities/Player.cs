using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]


public class Player : Entity
{
    private Rigidbody2D m_Body;
    private byte m_JumpCount = 0;
    private DashData m_DashData = new DashData();

    private Collision2D m_LastOtherCollision;

    public float m_Speed = 1.0f;
    public float m_JumpForce = 1.0f;


    void Start()
    {
        m_MaxHealth = 100f;
        m_Health = m_MaxHealth;
        m_IsFacingRight = false;

        m_Body = GetComponent<Rigidbody2D>();
        m_Animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (m_Health < 0f)
        {
            Die();
            return;
        }

        if (!Dash())
        {
            move();
            flip();
        }

        if (Input.GetButtonDown("Jump") && m_JumpCount < 2)
            jump();

        m_Animator.SetBool("Jumping", m_JumpCount > 0);
    }


    public void Die()
    {
        m_CheckpointManager.ReloadCheckPoint();
    }


    private bool Dash()
    { 
        switch (m_DashData.state)
        {
            case DashData.DashState.Ready:
                if (Input.GetKeyDown("left shift"))
                {
                    m_DashData.state = DashData.DashState.Dashing;
                    m_DashData.originX = transform.position.x;
                    m_DashData.targetX = transform.position.x + 5f * (m_IsFacingRight ? 1f : -1f);
                    m_DashData.t = 0f;
                    m_DashData.cooldown = 2f;

                    m_Animator.SetBool("Dashing", true);
                }

                break;

            case DashData.DashState.Cooldown:
                if (m_DashData.cooldown > 0f)
                    m_DashData.cooldown -= Time.deltaTime;

                else
                    m_DashData.state = DashData.DashState.Ready;
              
                break;

            case DashData.DashState.Dashing:
                m_DashData.t += Time.deltaTime / 0.2f;

                transform.position = Vector3.Lerp(new Vector3(m_DashData.originX, transform.position.y, transform.position.z), new Vector3(m_DashData.targetX, transform.position.y, transform.position.z), m_DashData.t);
                m_Body.velocity = Vector2.zero;

                if (transform.position.x == m_DashData.targetX || m_LastOtherCollision.collider.IsTouching(m_LastOtherCollision.otherCollider))
                {
                    m_DashData.state = DashData.DashState.Cooldown;
                    m_Animator.SetBool("Dashing", false);
                }

                return true;
        }

        return false;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        m_LastOtherCollision = collision;

        if (collision.GetContact(0).point.y < collision.otherCollider.bounds.min.y)
            m_JumpCount = 0;

        else

        if (m_DashData.state == DashData.DashState.Dashing)
        {
            m_DashData.state = DashData.DashState.Cooldown;
            m_Animator.SetBool("Dashing", false);
        }
    }
    

     private void move()
    {
        m_Body.velocity = new Vector2(Input.GetAxis("Horizontal") * m_Speed, m_Body.velocity.y);
        m_Animator.SetFloat("Velocity[x]", Mathf.Abs(m_Body.velocity.x));
    }


    private void jump()
    {
        if (m_JumpCount != 0)
            m_Body.velocity = new Vector2(m_Body.velocity.x, 0);

        m_Body.AddForce(new Vector2(0, m_JumpForce), ForceMode2D.Impulse);

        m_JumpCount++;
    }


    private void flip()
    {
        if ((Camera.main.WorldToScreenPoint(transform.position).x < Input.mousePosition.x && !m_IsFacingRight) || (Camera.main.WorldToScreenPoint(transform.position).x > Input.mousePosition.x && m_IsFacingRight))
        {
            m_IsFacingRight = !m_IsFacingRight;
            transform.localScale = Vector3.Reflect(transform.localScale, Vector3.left);
        }
    }


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
}