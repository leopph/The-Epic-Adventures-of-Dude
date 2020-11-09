using UnityEngine;


public class TriggerZone : GameManager
{
    public GameObject[] m_Objects;
    public float m_Range;

    private Transform m_Player;


    private void Start()
    {
        m_CheckpointManager.Register(this);
        m_Player = GameObject.FindWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (m_Player.position.x >= transform.position.x)
        {
            foreach (GameObject gameObject in m_Objects)
                gameObject.SetActive(true);

            m_CheckpointManager.QueueForRemoval(this);
        }
    }


    public override void Restart() {}
}
