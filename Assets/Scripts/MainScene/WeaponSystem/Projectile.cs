using UnityEngine;


public abstract class Projectile : MonoBehaviour
{
    protected float m_MinDMG;
    protected float m_MaxDMG;


    protected abstract void Start();


    public float Damage()
    {
        return (m_MaxDMG - m_MinDMG) * Random.value + m_MinDMG;
    }
}
