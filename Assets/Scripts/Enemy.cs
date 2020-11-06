using UnityEngine;


public class Enemy : Entity
{
    void Start()
    {
        m_MaxHealth = 100f;
        m_Health = m_MaxHealth;

        m_CheckpointManager.Register(this, transform.position);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
            if (m_Health - 33.3f < 0f)
            {
                m_Health = 0;
                gameObject.SetActive(false);
            }
            else m_Health -= 33.3f;

        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().Die();
    }
}
