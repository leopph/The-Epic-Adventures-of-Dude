using UnityEngine;


public abstract class GameManager : MonoBehaviour
{
    protected static CheckpointManager m_CheckpointManager;


    protected void Awake()
    {
        m_CheckpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
    }


    public abstract void Restart();
}
