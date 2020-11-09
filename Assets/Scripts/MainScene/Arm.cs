using UnityEngine;


public class Arm : MonoBehaviour
{
    private Transform m_ShoulderPoint;
    private Player m_Player;


    // Start is called before the first frame update
    void Start()
    {
        m_ShoulderPoint = transform.parent;
        m_Player = GetComponentInParent<Player>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_ShoulderPoint.position;

        if ((direction.x > 0 && !m_Player.FacingRight()) || (direction.x < 0 && m_Player.FacingRight()))
            direction.x = 0;

        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, direction));
    }
}
