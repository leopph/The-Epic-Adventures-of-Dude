using UnityEngine;
using UnityEngine.Assertions;


[RequireComponent(typeof(EnemyPathfinding))]


public class EnemyAI : MonoBehaviour
{
    private enum State { Idle, Chasing, Combat, Retreating }

    private State m_State = State.Idle;
    private Vector3 m_StartingPosition;
    private Player m_Player;
    private EnemyPathfinding m_Pathfinding;
    private EnemyAttack m_EnemyAttack;
    private Animator m_Animator;

    public float m_IdleWalkDistance;
    public float m_ChaseRange;
    public float m_AttackRange;
    public float m_MaxDistanceFromStart;


    private void Awake()
    {
        Assert.IsTrue(m_IdleWalkDistance <= m_MaxDistanceFromStart, "MAX WALK DISTANCE IS BIGGER THAN THE ALLOWED MAX DISTANCE FROM SPAWN");

        m_StartingPosition = transform.position;
        m_Pathfinding = GetComponent<EnemyPathfinding>();
        m_EnemyAttack = GetComponent<EnemyAttack>();
    }

    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
        m_Player = GameObject.Find("Dood").GetComponent<Player>();
    }


    void Update()
    {
        if (m_State != State.Retreating && Mathf.Abs(transform.position.x - m_StartingPosition.x) > m_MaxDistanceFromStart)
            ChangeState(State.Retreating);

        switch (m_State)
        {
            case State.Idle:
                {
                    if (Mathf.Abs(transform.position.x - m_Player.transform.position.x) <= m_ChaseRange && Mathf.Abs(m_StartingPosition.x - m_Player.transform.position.x) <= m_StartingPosition.x + m_MaxDistanceFromStart)
                    {
                        ChangeState(State.Chasing);
                    }
                    else if (!m_Pathfinding.moving)
                    {
                        float distance = transform.position.x - m_StartingPosition.x;
                        float newDistance = Random.value * m_IdleWalkDistance * (distance > 0 ? -1 : 1);

                        m_Pathfinding.MoveTo(m_StartingPosition + new Vector3(newDistance, 0));
                    }
                    break;
                }


            case State.Chasing:
                {
                    if (Mathf.Abs(m_Player.transform.position.x - transform.position.x) <= m_AttackRange)
                    {
                        ChangeState(State.Combat);
                    }
                    else if (!m_Pathfinding.moving)
                    {
                        m_Pathfinding.Track(m_Player.transform);
                    }
                    break;
                }


            case State.Combat:
                {
                    if (Mathf.Abs(m_Player.transform.position.x - transform.position.x) > m_AttackRange && !m_EnemyAttack.attacking)
                    {
                        ChangeState(State.Chasing);
                    }
                    else
                    {
                        if (!m_EnemyAttack.attacking)
                            m_EnemyAttack.Attack(m_Player);
                    }
                    break;
                }


            case State.Retreating:
                {
                    if (!m_Pathfinding.moving)
                    {
                        if (Mathf.Abs(m_StartingPosition.x - transform.position.x) <= m_IdleWalkDistance)
                        {
                            ChangeState(State.Idle);
                        }
                        else
                        {
                            Vector3 target = m_StartingPosition + new Vector3(transform.position.x > m_StartingPosition.x ? m_IdleWalkDistance : -m_IdleWalkDistance, 0);
                            m_Pathfinding.MoveTo(target);
                        }
                    }
                    break;
                }
        }

        m_Animator.SetBool("Moving", m_Pathfinding.moving);
    }



    private void ChangeState(State newState)
    {
        m_Pathfinding.StopMoving();
        m_State = newState;
        Debug.Log(newState);
    }
}
