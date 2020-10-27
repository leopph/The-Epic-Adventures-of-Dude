using UnityEngine;


public class Movement : MonoBehaviour
{
    private Rigidbody2D m_Body;
    private Animator m_Animator;
    private byte m_JumpCount = 0;
    private bool m_IsFacingRight = false;

    public float m_Speed = 1.0f;
    public float m_JumpForce = 1.0f;


    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
        m_Animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if ((Camera.main.WorldToScreenPoint(transform.position).x < Input.mousePosition.x && !m_IsFacingRight) || (Camera.main.WorldToScreenPoint(transform.position).x > Input.mousePosition.x && m_IsFacingRight))
            flip();

        move();

        if (Input.GetButtonDown("Jump") && m_JumpCount < 2)
            jump();

        m_Animator.SetBool("Jumping", m_JumpCount > 0);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).point.y < collision.otherCollider.bounds.min.y)
            m_JumpCount = 0;
    }


    public bool FacingRight()
    {
        return m_IsFacingRight;
    }
    

     private void move()
    {
        float horiz = Input.GetAxis("Horizontal");

        m_Body.velocity = new Vector2(horiz * m_Speed, m_Body.velocity.y);
        m_Animator.SetFloat("Velocity[x]", Mathf.Abs(m_Body.velocity.x));
    }


    private void jump()
    {
        if (m_JumpCount != 0)
            m_Body.velocity = Vector2.zero;

        m_Body.AddForce(new Vector2(0, m_JumpForce), ForceMode2D.Impulse);

        m_JumpCount++;
    }


    private void flip()
    {
        m_IsFacingRight = !m_IsFacingRight;
        transform.localScale = Vector3.Reflect(transform.localScale, Vector3.left);
    }
}
