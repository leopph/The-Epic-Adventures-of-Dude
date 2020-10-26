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
        // left
        if (!m_Left && Camera.main.transform.position.x - m_CameraWidth / 2f <= transform.position.x - m_SpriteRenderer.sprite.bounds.extents.x / 2f)
        {
            GameObject tile = Instantiate(gameObject, transform.position - new Vector3(2 * m_SpriteRenderer.sprite.bounds.extents.x, 0, 0), transform.rotation);
            tile.transform.localScale = Vector3.Reflect(tile.transform.localScale, Vector3.left);
            tile.transform.parent = transform.parent;
            
            m_Left = true;

            tile.GetComponent<Tiling>().m_Right = true;
            tile.GetComponent<Tiling>().m_Dood = m_Dood;

        }
        // right
        else if (!m_Right && Camera.main.transform.position.x + m_CameraWidth / 2f >= transform.position.x + m_SpriteRenderer.sprite.bounds.extents.x / 2f)
        {

            GameObject tile = Instantiate(gameObject, transform.position + new Vector3(2 * m_SpriteRenderer.sprite.bounds.extents.x, 0, 0), transform.rotation);
            tile.transform.localScale = Vector3.Reflect(tile.transform.localScale, Vector3.left);
            tile.transform.parent = transform.parent;

            m_Right = true;

            tile.GetComponent<Tiling>().m_Left = true;
            tile.GetComponent<Tiling>().m_Dood = m_Dood;
        }
    }
}
