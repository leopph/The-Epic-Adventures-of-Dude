using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float m_Health = 100f;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProj")
        {
            if (m_Health - 33.3f < 0f)
            {
                m_Health = 0;
                gameObject.SetActive(false);
            }
            else m_Health -= 33.3f;
            
            collision.gameObject.SetActive(false);
        }
    }
}
