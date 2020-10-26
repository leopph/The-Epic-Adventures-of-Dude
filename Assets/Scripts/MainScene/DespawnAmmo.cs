using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnAmmo : MonoBehaviour
{
    private float m_TimeLeft = 0f;
    public float m_LifeTime = 5f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        //GetComponent<BoxCollider2D>().enabled = false;
        //GetComponent<Rigidbody2D>().simulated = false;
    }

    public float TimeLeft() { return m_TimeLeft; }

    void OnEnable() { m_TimeLeft = m_LifeTime; }

    void Update()
    {
        m_TimeLeft -= Time.deltaTime;
        if (this.m_TimeLeft <= 0f)
            gameObject.SetActive(false);
    }
}
