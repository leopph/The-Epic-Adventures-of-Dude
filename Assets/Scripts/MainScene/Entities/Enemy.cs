using UnityEngine;


//[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Collider))]


public class Enemy : Entity
{
    private float m_PlayerContactTime = 0f;
    private float m_MinDamage = 20f;
    private float m_MaxDamage = 30f;




    protected void Awake()
    {
        m_MaxHealth = 100f;
        m_Health = m_MaxHealth;
    }



    protected override void Start()
    {
        base.Start();
        m_Animator = GetComponentInChildren<Animator>();
        m_CheckpointManager.Register(this, transform.position);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().Damage());
            m_AudioManager.PlaySound("DemonHurt");
        }    

        /*else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(Random.Range(m_MinDamage, m_MaxDamage));
            m_PlayerContactTime = 0f;
        }*/
    }



    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_PlayerContactTime += Time.deltaTime;

            if (m_PlayerContactTime > 1f)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(Random.Range(m_MinDamage, m_MaxDamage));
                m_PlayerContactTime = 0f;
            }
        }
    }*/



    public override void Die()
    {
        EventSystem.current.EnemyKilled();

        foreach (Projectile projectile in GetComponentsInChildren<Projectile>())
                projectile.Refresh();

        m_CheckpointManager.QueueForRemoval(this);
        gameObject.SetActive(false);
    }
}
