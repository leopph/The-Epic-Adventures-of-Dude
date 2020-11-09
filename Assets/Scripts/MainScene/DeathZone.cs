using UnityEngine;


[RequireComponent(typeof(EdgeCollider2D))]


public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().Die();
    }
}
