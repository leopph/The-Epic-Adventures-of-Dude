using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider))]


public class Enemy : Entity
{
    private GameObject m_Player;
    private Transform m_BodyTransform;




    protected void Awake()
    {
        m_MaxHealth = 100f;
        m_Health = m_MaxHealth;
        m_IsFacingRight = true;
    }



    protected override void Start()
    {
        base.Start();
        m_Animator = GetComponent<Animator>();
        m_Player = GameObject.FindWithTag("Player");
        m_CheckpointManager.Register(this, transform.position);

        m_BodyTransform = transform.GetChild(0);
    }



    private void Update()
    {
        if ((transform.position.x > m_Player.transform.position.x && m_IsFacingRight) || (transform.position.x < m_Player.transform.position.x && !m_IsFacingRight))
        {
            m_IsFacingRight = !m_IsFacingRight;
            m_BodyTransform.localScale = Vector3.Reflect(m_BodyTransform.localScale, Vector3.left);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
            TakeDamage(collision.gameObject.GetComponent<Projectile>().Damage());

        else if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().TakeDamage(Random.Range(30f, 35f));
    }



    public override void Die()
    {
        foreach (Projectile projectile in GetComponentsInChildren<Projectile>())
                projectile.Refresh();

        m_CheckpointManager.QueueForRemoval(this);
        gameObject.SetActive(false);
    }
}
