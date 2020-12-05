using UnityEngine;


[RequireComponent(typeof(Collider2D))]


public class Arrow : Projectile
{
    protected override void Awake()
    {
        base.Awake();

        m_MinDMG = 30f;
        m_MaxDMG = 50f;

        m_Rigidbody.centerOfMass = new Vector2(0.15f, 0f);
        m_Rigidbody.simulated = false;
    }



    private void FixedUpdate()
    {
        if (m_Rigidbody.simulated)
            transform.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(m_Rigidbody.velocity, Vector2.right));
    }



    void Update()
    {
        m_TimeLeft -= Time.deltaTime;

        if (m_TimeLeft <= 0f)
            Refresh();
    }



    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == gameObject.tag)
            return;

        base.OnCollisionEnter2D(collision);

        m_Rigidbody.simulated = false;
        transform.parent = collision.transform;

        m_AudioManager.PlaySound("Explody");
    }



    public float TimeLeft() { return m_TimeLeft; }
}
