using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]


public class Tiling : MonoBehaviour
{
    public bool m_IsRandomizedX = false;
    public float m_MaxSpaceX = 1f;
    public bool m_IsRandomizedY = false;
    public float m_MaxSpaceY = 2f;
    public Sprite[] m_Sprites;

    private GameObject m_Proto;
    private SpriteRenderer m_SpriteRenderer;
    private static float s_CameraWidth;
    private bool m_Left = false;
    private bool m_Right = false;
    private float m_OffsetY = 0f;


    // Start is called before the first frame update
    void Start()
    {
        if (m_Proto == null)
        {
            m_Proto = Instantiate(gameObject);
            m_Proto.SetActive(false);
        }

        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        if (m_Sprites.Length == 0)
        {
            m_Sprites = new Sprite[1];
            m_Sprites[0] = m_SpriteRenderer.sprite;
        }

        AssignRandomSprite();
        s_CameraWidth = Camera.main.ViewportToWorldPoint(Vector3.one).x - Camera.main.ViewportToWorldPoint(Vector3.zero).x;
    }


    // Update is called once per frame
    void Update()
    {
        bool leftSpawn = !m_Left && Camera.main.transform.position.x - s_CameraWidth / 2f <= transform.position.x - m_SpriteRenderer.sprite.bounds.extents.x / 2f;
        bool rightSpawn = !m_Right && Camera.main.transform.position.x + s_CameraWidth / 2f >= transform.position.x + m_SpriteRenderer.sprite.bounds.extents.x / 2f;

        if (leftSpawn || rightSpawn)
        {
            float offsetX = 0f;
            float offsetY = 0f;

            if (m_IsRandomizedX)
                offsetX = (leftSpawn ? -1f : 1f) * m_MaxSpaceX * Random.value;

            if (m_IsRandomizedY)
                offsetY = 2f * (m_MaxSpaceY * Random.value - m_MaxSpaceY / 2f);

            Vector3 position;

            if (leftSpawn)
                position = transform.position + new Vector3(-2f * m_SpriteRenderer.sprite.bounds.extents.x + offsetX, offsetY - m_OffsetY, 0);
            else
                position = transform.position + new Vector3(2f * m_SpriteRenderer.sprite.bounds.extents.x + offsetX, offsetY - m_OffsetY, 0);

            GameObject tile = Instantiate(m_Proto, position, transform.rotation);
            tile.SetActive(true);
            tile.transform.parent = transform.parent;

            Tiling tiling = tile.GetComponent<Tiling>();
            tiling.m_OffsetY = offsetY;
            tiling.m_Proto = m_Proto;

            if (leftSpawn)
            {
                m_Left = true;
                tiling.m_Right = true;
            }
            else
            {
                m_Right = true;
                tiling.m_Left = true;
            }
        }
    }


    private void AssignRandomSprite()
    {
        m_SpriteRenderer.sprite =  m_Sprites[(int)((Random.value * m_Sprites.Length) % m_Sprites.Length)];
    }
}
