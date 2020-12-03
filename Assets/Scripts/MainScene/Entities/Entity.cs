using UnityEngine;


public abstract class Entity : MonoBehaviour
{
    protected float m_MaxHealth;
    protected float m_Health;
    protected bool m_IsFacingRight;
    protected Animator m_Animator;

    protected static CheckpointManager m_CheckpointManager;
    protected static AudioManager m_AudioManager;



    protected virtual void Start()
    {
        m_CheckpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }



    public void ResetHealth()
    {
        m_Health = m_MaxHealth;
    }



    public bool FacingRight()
    {
        return m_IsFacingRight;
    }



    public abstract void Die();
}
