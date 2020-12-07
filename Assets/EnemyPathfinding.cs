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




    private void Awake() { m_Seeker = GetComponent<Seeker>(); }



    private void Update()
    {
        if (!m_Moving)
            return;

        if (m_Path == null)
            return;

        if (m_CurrentWaypoint >= m_Path.vectorPath.Count)
        {
            if (Mathf.Approximately(transform.position.x, m_Target.x))
                StopMoving();
            else
            {
                if (Mathf.Abs(transform.position.x - m_Target.x) < m_Speed * Time.deltaTime)
                    transform.position = new Vector3(m_Target.x, transform.position.y, transform.position.z);
                else
                    transform.Translate(new Vector2(m_Target.x > transform.position.x ? 1 : -1, 0) * m_Speed * Time.deltaTime);
            }
            return;
        }

        transform.Translate(new Vector2(m_Path.vectorPath[m_CurrentWaypoint].x - transform.position.x, 0).normalized * m_Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, m_Path.vectorPath[m_CurrentWaypoint]) < m_NextWaypointDistance)
            m_CurrentWaypoint++;
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



    public void TargetUpdate() { m_Target = m_TrackedTarget.position; Debug.Log(m_Target); }



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
