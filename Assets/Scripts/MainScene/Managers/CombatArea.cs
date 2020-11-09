using UnityEngine;


public class CombatArea : TriggerZone
{
    public EnemySpawner[] m_Spawners;

    private CombatState m_State = CombatState.Sleeping;


    // Update is called once per frame
    private void Update()
    {
        if (m_State != CombatState.Finished)
            switch (m_State)
            {
                case CombatState.Sleeping:
                    if (PlayerInZone())
                    {
                        m_State = CombatState.Active;

                        foreach (GameObject gameObject in m_Objects)
                            gameObject.SetActive(true);
                    }
                    break;

                case CombatState.Active:
                    bool spawnersDone = true;

                    foreach (EnemySpawner spawner in m_Spawners)
                        if (spawner.State() != EnemySpawner.SpawnerState.SpawnerCleared)
                        {
                            spawnersDone = false;
                            break;
                        }

                    if (spawnersDone)
                    {
                        foreach (GameObject gameObject in m_Objects)
                            gameObject.SetActive(false);

                        m_State = CombatState.Finished;
                    }
                    break;
            }
    }


    public override void Restart()
    {
        m_State = CombatState.Sleeping;

        foreach (GameObject gameObject in m_Objects)
            gameObject.SetActive(false);
    }


    private enum CombatState
    {
        Sleeping,
        Active,
        Finished
    }
}
