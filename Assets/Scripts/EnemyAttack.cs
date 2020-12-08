using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;




public class EnemyAttack : MonoBehaviour
{
    public Rigidbody2D m_Projectile;
    private List<Rigidbody2D> m_ProjectilePool;
    public float m_ProjectileSpeed;
    public float m_PoolSize;

    private Animator m_Animator;
    private bool m_Attacking = false;
    public bool attacking => m_Attacking;

    private float m_DeltaTime = 0f;
    public float m_AttackSpeed = 1f;

    public bool m_Melee = false;
    public float m_MinMeleeDamage = 5f;
    public float m_MaxMeleeDamage = 25f;
    private bool m_MeleeAttackDone = false;


    private Entity m_Target;




    private void Awake()
    {
        Assert.IsTrue(m_Melee || m_Projectile != null, "THIS UNIT IS RANGED BUT DOES NOT HAVE A PROJETILE SET");

        if (!m_Melee)
        {
            m_ProjectilePool = new List<Rigidbody2D>();
            for (int i = 0; i < m_PoolSize; i++)
            {
                m_ProjectilePool.Add(Instantiate(m_Projectile));
                m_ProjectilePool[i].gameObject.SetActive(false);
            }
        }
    }



    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
    }



    private void Update()
    {
        if (attacking)
        {
            m_DeltaTime += Time.deltaTime;

            if (m_Melee && !m_MeleeAttackDone && m_DeltaTime >= 1 / m_AttackSpeed / 2)
            {
                m_Target.TakeDamage(Random.Range(m_MinMeleeDamage, m_MaxMeleeDamage));
                m_MeleeAttackDone = true;
            }

            if (m_DeltaTime >= 1 / m_AttackSpeed)
                m_Attacking = false;
        }
    }



    public void Attack(Entity target)
    {
        m_Animator.SetFloat("AttackSpeed", m_AttackSpeed);
        m_Animator.SetTrigger("Attacking");
        m_Attacking = true;
        m_DeltaTime = 0f;

        m_Target = target;

        if (!m_Melee)
            Shoot();
        else
            m_MeleeAttackDone = false;
    }



    private void Shoot()
    {
        Rigidbody2D projectile = GetProjectileFromPool();

        Vector2 direction = (m_Target.transform.position - transform.position).normalized;

        projectile.gameObject.GetComponent<Projectile>().Refresh();
        projectile.gameObject.SetActive(true);
        projectile.transform.position = transform.position;
        projectile.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));
        projectile.velocity = m_ProjectileSpeed * direction;

        //m_AudioManager.PlaySound("");
    }


    private Rigidbody2D GetProjectileFromPool()
    {
        foreach (Rigidbody2D projectile in m_ProjectilePool)
            if (!projectile.gameObject.activeSelf)
                return projectile;

        Debug.LogError("THERES NO AVAILABLE PROJECTILE AND THIS CONTROL PATH NEEDS IMPLEMENTATION");
        // TODO
        return null;
    }
}
