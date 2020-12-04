using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ParticleSystem))]


public abstract class Projectile : MonoBehaviour
{
    public float m_LifeTime = 69f;
    protected float m_TimeLeft = 0f;

    protected float m_MinDMG;
    protected float m_MaxDMG;

    protected Rigidbody2D m_Rigidbody;
    protected AudioManager m_AudioManager;
    protected ParticleSystem m_ParticleSystem;




    protected virtual void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_ParticleSystem = GetComponent<ParticleSystem>();
        m_ParticleSystem.Play();
    }



    protected void Start()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }



    public float Damage()
    {
        return (m_MaxDMG - m_MinDMG) * Random.value + m_MinDMG;
    }



    public void Refresh()
    {
        m_ParticleSystem.Play();
        m_TimeLeft = m_LifeTime;
        m_Rigidbody.simulated = true;
        transform.parent = null;
        transform.DetachChildren();
        gameObject.SetActive(false);
    }



    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        m_ParticleSystem.Stop();
    }



    public static void RefreshAll()
    {
        foreach (Projectile projectile in FindObjectsOfType<Projectile>())
            projectile.Refresh();
    }
}
