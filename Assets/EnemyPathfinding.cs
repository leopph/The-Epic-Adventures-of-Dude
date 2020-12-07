using UnityEngine;
using Pathfinding;


[RequireComponent(typeof(Rigidbody2D))]


public class EnemyPathfinding : MonoBehaviour
{
    public float m_Speed = 2f;
    public bool m_VerticalMovement = false;

    private Path m_Path;
    private Seeker m_Seeker;

    private float m_NextWaypointDistance = 0.5f;
    private int m_CurrentWaypoint = 0;

    private Vector2 m_Target;
    private Transform m_TrackedTarget;
    private bool m_Moving = false;
    public bool moving => m_Moving;

    private ContactPoint2D[] m_CollisionPoints = new ContactPoint2D[2];

    public SpriteRenderer m_SpriteRenderer;




    private void Awake() { m_Seeker = GetComponent<Seeker>(); }



    private void Update()
    {
        if (!m_Moving)
            return;

        if (m_Path == null)
            return;

        if (m_CurrentWaypoint < m_Path.vectorPath.Count && Vector2.Distance(transform.position, m_Path.vectorPath[m_CurrentWaypoint]) < m_NextWaypointDistance)
            m_CurrentWaypoint++;

        Vector3 velocity = Vector3.zero;

        if (m_CurrentWaypoint >= m_Path.vectorPath.Count)
        {
            if (transform.position.x == m_Target.x)
            {
                StopMoving();
            }
            else
            {
                if (Mathf.Abs(transform.position.x - m_Target.x) < m_Speed * Time.deltaTime)
                {
                    transform.position = new Vector3(m_Target.x, transform.position.y, transform.position.z);
                }
                else
                {
                    velocity.x = m_Target.x > transform.position.x ? 1 : -1;
                }
            }
        }
        else
        {
            velocity = new Vector2(m_Path.vectorPath[m_CurrentWaypoint].x - transform.position.x, 0).normalized;
        }

        transform.Translate(velocity * m_Speed * Time.deltaTime);

        if (velocity.x > 0)
            m_SpriteRenderer.flipX = true;
        else if (velocity.x < 0)
            m_SpriteRenderer.flipX = false;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contactCount > m_CollisionPoints.Length)
            m_CollisionPoints = new ContactPoint2D[m_CollisionPoints.Length * 2];

        collision.GetContacts(m_CollisionPoints);

        foreach (ContactPoint2D p in m_CollisionPoints)
            if (p.point.y > collision.otherCollider.bounds.min.y)
            {
                StopMoving();
                return;
            }
    }



    public void MoveTo(Vector2 position)
    {
        StopMoving();

        m_Target = m_VerticalMovement ? position : new Vector2(position.x, transform.position.y);
        m_Moving = true;
        m_CurrentWaypoint = 0;
        InvokeRepeating("PathUpdate", 0f, 0.5f);
    }


    public void Track(Transform transform)
    {
        m_TrackedTarget = transform;
        MoveTo(m_Target);
        InvokeRepeating("TargetUpdate", 0f, 0.25f);
    }



    public void StopMoving()
    {
        CancelInvoke("PathUpdate");
        CancelInvoke("TargetUpdate");
        m_Moving = false;
    }



    public void TargetUpdate() { m_Target = m_TrackedTarget.position; }



    private void PathUpdate()
    {
        if (m_Seeker.IsDone())
        {
            if (!m_VerticalMovement)
                m_Target.y = transform.position.y;

            m_Seeker.StartPath(transform.position, m_Target, OnPathComplete);
        }
    }



    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            m_Path = path;
            m_CurrentWaypoint = 0; 
        }
    }
}
