using UnityEngine;


public abstract class Entity : MonoBehaviour
{
    protected float m_MaxHealth;
    protected float m_Health;
    protected bool m_IsFacingRight;
    protected Animator m_Animator;
    protected HealthBar m_HealthBar;

    protected static CheckpointManager m_CheckpointManager;
    protected static AudioManager m_AudioManager;




    protected virtual void Start()
    {
        m_CheckpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_HealthBar = GetComponentInChildren<HealthBar>();
        m_HealthBar.Init(m_MaxHealth);
    }



    protected virtual void Update()
    {
        if (m_Health <= 0f)
            Die();
    }



    public void ResetHealth()
    {
        m_Health = m_MaxHealth;

        if (!m_HealthBar)
        {
            m_HealthBar = GetComponentInChildren<HealthBar>();
            m_HealthBar.Init(m_MaxHealth);
        }

        m_HealthBar.Set(100);
    }

    public bool FacingRight() { return m_IsFacingRight; }

    public abstract void Die();

    public void TakeDamage(float damage)
    {
        m_Health -= damage;
        m_Animator.SetTrigger("Hurt");
        m_HealthBar.Set(m_Health);
    }
}
