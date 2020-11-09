using UnityEngine;


public class RoomCamera : MonoBehaviour
{
    public Transform target = null;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            if (target.position.x > Camera.main.ViewportToWorldPoint(Vector3.one).x)
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1.5f, 0.5f, 0));

            else if (target.position.x < Camera.main.ViewportToWorldPoint(Vector3.zero).x)
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(-1.5f, 0.5f, 0)); 

            else if (target.position.y > Camera.main.ViewportToWorldPoint(Vector3.one).y)
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.5f, 0));

            else if (target.position.y < Camera.main.ViewportToWorldPoint(Vector3.zero).y)
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, -1.5f, 0));
        }
    }
}
