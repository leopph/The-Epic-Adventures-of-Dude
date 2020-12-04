using System.Collections.Generic;
using UnityEngine;


public class CheckpointManager : MonoBehaviour
{
    private RespawnEntry m_Player;
    private HashSet<RespawnEntry> m_Enemies;
    private HashSet<GameManager> m_Managers;
    private HashSet<RespawnEntry> m_EnemiesToRemove;
    private HashSet<GameManager> m_ManagersToRemove;


    // Start is called before the first frame update
    void Awake()
    {
        m_Enemies = new HashSet<RespawnEntry>();
        m_Managers = new HashSet<GameManager>();

        m_EnemiesToRemove = new HashSet<RespawnEntry>();
        m_ManagersToRemove = new HashSet<GameManager>();

        GameObject player = GameObject.FindWithTag("Player");
        m_Player = new RespawnEntry(player.GetComponent<Player>(), player.transform.position);
    }


    public void Register(GameManager manager)
    {
        m_Managers.Add(manager);
    }

    public void Register(Entity gameObject, Vector3 position)
    {
        m_Enemies.Add(new RespawnEntry(gameObject, position));
    }


    public void QueueForRemoval(GameManager manager)
    {
        m_ManagersToRemove.Add(manager);
    }

    public void QueueForRemoval(Entity entity)
    {
        foreach (RespawnEntry entry in m_Enemies)
            if (entry.entity == entity)
            {
                m_EnemiesToRemove.Add(entry);
                return;
            }
    }


    public void SetCheckpoint(Checkpoint checkpoint)
    {
        Debug.Log("Checkpoint reached.");

        m_Player.position = checkpoint.transform.position;

        foreach (RespawnEntry entry in m_EnemiesToRemove)
            m_Enemies.Remove(entry);
        m_EnemiesToRemove.Clear();

        foreach (GameManager manager in m_ManagersToRemove)
            m_Managers.Remove(manager);
        m_ManagersToRemove.Clear();
    }


    public void ReloadCheckPoint()
    {
        m_Player.entity.transform.position = m_Player.position;
        m_Player.entity.ResetHealth();

        Projectile.RefreshAll();

        foreach (RespawnEntry entry in m_Enemies)
        {
            entry.entity.transform.position = entry.position;
            entry.entity.ResetHealth();
        }

        foreach (GameManager manager in m_Managers)
            manager.Restart();
    }


    public struct RespawnEntry
    {
        public Entity entity;
        public Vector3 position;


        public RespawnEntry(Entity entity, Vector3 position)
        {
            this.entity = entity;
            this.position = position;
        }
    }
}
