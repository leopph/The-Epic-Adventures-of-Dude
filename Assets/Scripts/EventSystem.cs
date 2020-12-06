using UnityEngine;
using System;




public class EventSystem : MonoBehaviour
{
    private static EventSystem m_Instance;
    public static EventSystem current => m_Instance;

    public event Action onEnemyKilled;
    public event Action onFallenToDeath;


    private void Awake()
    {
        if (m_Instance != null)
        {
            if (m_Instance != this)
                Destroy(this);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(this);
        }
    }


    public void EnemyKilled()
    {
        if (onEnemyKilled != null)
            onEnemyKilled();
    }


    public void FallenToDeath()
    {
        if (onFallenToDeath != null)
            onFallenToDeath();
    }
}
