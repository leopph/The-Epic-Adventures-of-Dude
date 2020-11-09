using UnityEngine;


public class TriggerZone : GameManager
{
    public GameObject[] m_Objects;
    public float m_Range;

    protected Transform m_Player;


    protected virtual void Start()
    {
        m_CheckpointManager.Register(this);
        m_Player = GameObject.FindWithTag("Player").transform;
    }


    // Update is called once per frame
    private void Update()
    {
        if (PlayerInZone())
        {
            foreach (GameObject gameObject in m_Objects)
                gameObject.SetActive(true);

            m_CheckpointManager.QueueForRemoval(this);
        }
    }


    protected bool PlayerInZone()
    {
        return m_Player.position.x >= transform.position.x && m_Player.position.x <= transform.position.x + m_Range;
    }


    public override void Restart() {}
}
