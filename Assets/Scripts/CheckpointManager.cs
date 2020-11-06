using System.Collections.Generic;
using UnityEngine;


public class CheckpointManager : MonoBehaviour
{
    private RespawnEntry m_Player;
    private List<RespawnEntry> m_Enemies;
    private List<GameManager> m_Managers;


    // Start is called before the first frame update
    void Awake()
    {
        m_Enemies = new List<RespawnEntry>();
        m_Managers = new List<GameManager>();

        GameObject player = GameObject.FindWithTag("Player");
        m_Player = new RespawnEntry(player.GetComponent<Player>(), player.transform.position, true);
    }


    public void Register(GameManager manager)
    {
        m_Managers.Add(manager);
    }


    public void Register(Entity gameObject, Vector3 position)
    {
        m_Enemies.Add(new RespawnEntry(gameObject, position, true));
    }


    public void ReloadCheckPoint()
    {
        m_Player.entity.transform.position = m_Player.position;

        foreach (RespawnEntry entry in m_Enemies)
            if (entry.needsRespawning)
            {
                entry.entity.transform.position = entry.position;
                entry.entity.ResetHealth();
            }

        foreach (GameManager manager in m_Managers)
            manager.Restart();

        foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("PlayerProjectile"))
        {
            projectile.transform.parent = null;
            projectile.SetActive(false);
        }
    }


    public struct RespawnEntry
    {
        public Entity entity;
        public Vector3 position;
        public bool needsRespawning;


        public RespawnEntry(Entity entity, Vector3 position, bool respawn)
        {
            this.entity = entity;
            this.position = position;
            needsRespawning = respawn;
        }
    }
}
