using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]


public class Arrow : MonoBehaviour
{
    public float m_LifeTime = 69f;

    private float m_TimeLeft = 0f;
    private Rigidbody2D m_Rigidbody;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Rigidbody.centerOfMass = new Vector2(1f, 0f);
        m_Rigidbody.simulated = false;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        m_Rigidbody.simulated = false;
        transform.parent = collision.transform;
    }


    public float TimeLeft() { return m_TimeLeft; }


    void OnEnable()
    {
        m_TimeLeft = m_LifeTime;
    }


    void Update()
    {
        m_TimeLeft -= Time.deltaTime;

        if (m_TimeLeft <= 0f)
            gameObject.SetActive(false);
    }
}
