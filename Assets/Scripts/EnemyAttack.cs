using UnityEngine;




public class EnemyAttack : MonoBehaviour
{
    private Animator m_Animator;
    private bool m_Attacking = false;
    public bool attacking => m_Attacking;

    private float m_DeltaTime = 0f;
    public float m_AttackSpeed = 1f;




    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        if (attacking)
        {
            m_DeltaTime += Time.deltaTime;

            if (m_DeltaTime > 1 / m_AttackSpeed)
                m_Attacking = false;
        }
    }


    public void Attack()
    {
        m_Animator.SetFloat("AttackSpeed", m_AttackSpeed);
        m_Animator.SetTrigger("Attacking");
        m_Attacking = true;
        m_DeltaTime = 0f;
    }
}
