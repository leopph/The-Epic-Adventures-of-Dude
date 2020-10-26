using UnityEngine;


public class Tiling : MonoBehaviour
{
    public bool m_Left = false;
    public bool m_Right = false;
    public Transform m_Dood = null;

    private float m_CameraWidth;
    private SpriteRenderer m_SpriteRenderer = null;


    // Start is called before the first frame update
    void Start()
    {
        m_CameraWidth = Camera.main.ViewportToWorldPoint(Vector3.one).x - Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!m_Left && Camera.main.transform.position.x - m_CameraWidth / 2f <= transform.position.x - m_SpriteRenderer.sprite.bounds.extents.x / 2f)
        {
            GameObject leftTile = Instantiate(gameObject, new Vector3(transform.position.x - m_SpriteRenderer.sprite.bounds.extents.x * 2, transform.position.y, transform.position.z), transform.rotation);
            leftTile.transform.localScale = new Vector3(transform.localScale.x * - 1, transform.localScale.y, transform.localScale.z);

            leftTile.GetComponent<Tiling>().m_Right = true;
            m_Left = true;

            leftTile.GetComponent<Tiling>().m_Dood = m_Dood;

            leftTile.transform.parent = transform.parent;

        }
        else if (!m_Right && Camera.main.transform.position.x + m_CameraWidth / 2f >= transform.position.x + m_SpriteRenderer.sprite.bounds.extents.x / 2f)
        {

            GameObject rightTile = Instantiate(gameObject, new Vector3(transform.position.x + m_SpriteRenderer.sprite.bounds.extents.x * 2, transform.position.y, transform.position.z), transform.rotation);
            rightTile.transform.localScale = new Vector3(transform.localScale.x * - 1, transform.localScale.y, transform.localScale.z);

            rightTile.GetComponent<Tiling>().m_Left = true;
            m_Right = true;

            rightTile.GetComponent<Tiling>().m_Dood = m_Dood;

            rightTile.transform.parent = transform.parent;
        }
    }
}
