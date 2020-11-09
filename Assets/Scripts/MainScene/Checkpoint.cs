using UnityEngine;



public class Checkpoint : MonoBehaviour
{
    private CheckpointManager m_CheckpointManager;
    private Transform m_Player;


    // Start is called before the first frame update
    void Start()
    {
        m_CheckpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        m_Player = GameObject.FindWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (m_Player.position.x >= transform.position.x)
        {
            m_CheckpointManager.SetCheckpoint(this);
            gameObject.SetActive(false);
        }
    }
}
