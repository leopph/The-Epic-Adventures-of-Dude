using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider))]
//[RequireComponent(typeof(SpriteRenderer))]


public class Enemy : Entity
{
    private GameObject m_Player;


    void Start()
    {
        m_MaxHealth = 100f;
        m_Health = m_MaxHealth;
        m_IsFacingRight = true;

        m_Animator = GetComponent<Animator>();
        m_Player = GameObject.FindWithTag("Player");
        m_CheckpointManager.Register(this, transform.position);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            float dmg = collision.gameObject.GetComponent<Projectile>().Damage();

            if (m_Health - dmg < 0f)
            {
                m_Health = 0;
                m_CheckpointManager.QueueForRemoval(this);
                gameObject.SetActive(false);
            }
            else
            {
                m_Health -= dmg;
                m_Animator.SetTrigger("Hurt");
            }

        }

        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().Die();
    }


    private void Update()
    {
        if ((transform.position.x > m_Player.transform.position.x && m_IsFacingRight) || (transform.position.x < m_Player.transform.position.x && !m_IsFacingRight))
        {
            m_IsFacingRight = !m_IsFacingRight;
            transform.localScale = Vector3.Reflect(transform.localScale, Vector3.left);
        }
    }
}
