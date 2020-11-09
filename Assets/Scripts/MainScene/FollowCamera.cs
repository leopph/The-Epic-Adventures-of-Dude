using UnityEngine;


public class FollowCamera : MonoBehaviour
{
    public Transform m_Target = null;


    void Update()
    {
        transform.position = new Vector3(m_Target.position.x, transform.position.y, transform.position.z);
    }
}
