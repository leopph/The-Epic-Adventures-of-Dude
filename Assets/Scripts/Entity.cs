﻿using UnityEngine;


public abstract class Entity : MonoBehaviour
{
    protected float m_MaxHealth;
    protected float m_Health;
    protected static CheckpointManager m_CheckpointManager;


    protected void Awake()
    {
        m_CheckpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
    }


    public void ResetHealth()
    {
        m_Health = m_MaxHealth;
    }


    //public float GetHealth() { return m_Health; }
    //public float GetMaxHealth() { return m_MaxHealth; }
}
