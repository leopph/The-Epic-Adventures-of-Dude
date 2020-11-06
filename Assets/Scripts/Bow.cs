using System.Collections.Generic;
using UnityEngine;


public class Bow : MonoBehaviour
{
    public Rigidbody2D m_Ammo = null;
    public float m_ProjectileSpeed = 75f;
    public int m_AmmoPoolSize = 420;

    private List<Rigidbody2D> m_AmmoPool;


    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
        transform.position = transform.parent.position;

        m_AmmoPool = new List<Rigidbody2D>();

        for (int i = 0; i < m_AmmoPoolSize; i++)
        {
            m_AmmoPool.Add(Instantiate(m_Ammo));
            m_AmmoPool[i].gameObject.SetActive(false);
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Fire();
    }


    private void Fire()
    {
        Rigidbody2D ammo = GetAmmoFromPool();

        Vector2 direction = Quaternion.AngleAxis(transform.parent.parent.rotation.eulerAngles.z, Vector3.forward) * Vector2.down;

        ammo.gameObject.SetActive(true);
        ammo.GetComponent<Rigidbody2D>().simulated = true;
        ammo.transform.parent = null;
        ammo.transform.DetachChildren();
        ammo.transform.position = transform.position;
        ammo.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));
        ammo.velocity = m_ProjectileSpeed * direction;
    }

    private Rigidbody2D GetAmmoFromPool()
    {
        foreach (Rigidbody2D ammo in m_AmmoPool)
            if (!ammo.gameObject.activeSelf)
                return ammo;


        int oldest = -1;
        float timeLeft = float.MaxValue;

        for (int i = 0; i < m_AmmoPoolSize; i++)
        {
            if (m_AmmoPool[i].gameObject.GetComponent<Arrow>().TimeLeft() < timeLeft)
            {
                timeLeft = m_AmmoPool[i].gameObject.GetComponent<Arrow>().TimeLeft();
                oldest = i;
            }
        }

        m_AmmoPool[oldest].gameObject.SetActive(false);
        return m_AmmoPool[oldest];
    }
}
