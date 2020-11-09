using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]


public class EnemySpawner : GameManager
{
    public GameObject m_EnemyType;
    public int[] m_WaveAmounts;
    public float m_Range;

    private List<Entity> m_Enemies = new List<Entity>();
    private Transform m_Player;
    private int m_CurrentWave = -1;
    private SpawnerState m_State = SpawnerState.Sleeping;
    private Animator m_Animator;


    // Start is called before the first frame update
    void Start()
    {
        m_CheckpointManager.Register(this);

        m_Player = GameObject.FindWithTag("Player").transform;
        m_Animator = GetComponent<Animator>();

        int maxEnemiesAtOnce = 0;
        for (int i = 0; i < m_WaveAmounts.Length; i++)
            if (m_WaveAmounts[i] > maxEnemiesAtOnce)
                maxEnemiesAtOnce = m_WaveAmounts[i];

        for (int i = 0; i < maxEnemiesAtOnce; i++)
        {
            GameObject tmp = Instantiate(m_EnemyType);
            tmp.gameObject.SetActive(false);

            m_Enemies.Add(tmp.GetComponent<Entity>());
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (m_State != SpawnerState.Inactive && Vector3.Distance(m_Player.position, transform.position) < m_Range)
            switch (m_State)
            {
                case SpawnerState.Sleeping:
                    m_CurrentWave = 0;
                    m_State = SpawnerState.Spawning;
                    break;

                case SpawnerState.Spawning:
                    m_Animator.SetTrigger("Spawning");

                    for (int i = 0; i < m_WaveAmounts[m_CurrentWave]; i++)
                    {
                        m_Enemies[i].transform.position = transform.position;
                        m_Enemies[i].gameObject.SetActive(true);
                        m_Enemies[i].ResetHealth();
                    }

                    m_State = SpawnerState.WaitingForNextWave;
                    break;

                case SpawnerState.WaitingForNextWave:
                    int dead = 0;
                    for (int i = 0; i < m_WaveAmounts[m_CurrentWave]; i++)
                        if (!m_Enemies[i].gameObject.activeSelf)
                            dead++;

                    if (dead == m_WaveAmounts[m_CurrentWave])
                        m_State = SpawnerState.WaveCleared;
                    break;

                case SpawnerState.WaveCleared:
                    if (m_CurrentWave == m_WaveAmounts.Length - 1)
                        m_State = SpawnerState.SpawnerCleared;
                    else
                    {
                        m_CurrentWave++;
                        m_State = SpawnerState.Spawning;
                    }
                    break;

                case SpawnerState.SpawnerCleared:
                    m_Animator.SetBool("Destroyed", true);
                    m_CheckpointManager.QueueForRemoval(this);
                    m_State = SpawnerState.Inactive;
                    break;
            }
    }


    public override void Restart()
    {
        foreach (Entity enemy in m_Enemies)
            enemy.gameObject.SetActive(false);

        m_State = SpawnerState.Sleeping;
    }


    enum SpawnerState
    {
        Sleeping,
        Spawning,
        WaitingForNextWave,
        WaveCleared,
        SpawnerCleared,
        Inactive
    }
}
