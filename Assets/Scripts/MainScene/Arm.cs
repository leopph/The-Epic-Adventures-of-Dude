using UnityEngine;


public class Arm : MonoBehaviour
{
    private Transform m_ShoulderPoint;
    private Movement m_Movement;


    // Start is called before the first frame update
    void Start()
    {
        m_ShoulderPoint = transform.parent;
        m_Movement = GetComponentInParent<Movement>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_ShoulderPoint.position;

        if ((direction.x > 0 && !m_Movement.FacingRight()) || (direction.x < 0 && m_Movement.FacingRight()))
            direction.x = 0;

        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, direction));
    }
}
