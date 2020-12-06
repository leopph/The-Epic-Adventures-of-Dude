using UnityEngine;
using System;




public class EventSystem : MonoBehaviour
{
    private static EventSystem m_Instance;
    public static EventSystem current => m_Instance;


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


    public event Action OnEnemyKill;
    public void EnemyKill()
    {
        if (OnEnemyKill != null)
            OnEnemyKill();
    }
}
