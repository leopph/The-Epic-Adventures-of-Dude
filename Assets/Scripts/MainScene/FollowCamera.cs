using UnityEngine;


public class FollowCamera : MonoBehaviour
{
    public Transform m_Target = null;


    private void LateUpdate()
    {
        transform.position = new Vector3(m_Target.position.x, transform.position.y, transform.position.z);
    }
}
