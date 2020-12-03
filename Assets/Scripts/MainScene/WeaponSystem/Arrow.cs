using UnityEngine;


[RequireComponent(typeof(Collider2D))]


public class Arrow : Projectile
{
    protected override void Awake()
    {
        base.Awake();

        m_MinDMG = 30f;
        m_MaxDMG = 50f;

        m_Rigidbody.centerOfMass = new Vector2(1f, 0f);
        m_Rigidbody.simulated = false;
    }



    void Update()
    {
        m_TimeLeft -= Time.deltaTime;

        if (m_TimeLeft <= 0f)
            Refresh();
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        m_Rigidbody.simulated = false;
        transform.parent = collision.transform;

        m_AudioManager.Play("Explody");
    }



    public float TimeLeft() { return m_TimeLeft; }
}
