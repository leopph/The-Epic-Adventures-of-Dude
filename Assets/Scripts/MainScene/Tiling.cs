using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]


public class Tiling : MonoBehaviour
{
    public bool isRandomizedX = false;
    public bool isRandomizedY = false;
    public Sprite[] m_Sprites;

    private SpriteRenderer m_SpriteRenderer;
    private static float s_CameraWidth;
    private bool m_Left = false;
    private bool m_Right = false;
    private float m_OffsetY = 0f;


    // Start is called before the first frame update
    void Start()
    {
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

            if (isRandomizedX)
                offsetX = leftSpawn ? -1f : 1f * Random.value;

            if (isRandomizedY)
                offsetY = 2f * (2f * Random.value - 1f);

            Vector3 position;

            if (leftSpawn)
                position = transform.position + new Vector3(-2f * m_SpriteRenderer.sprite.bounds.extents.x + offsetX, offsetY - m_OffsetY, 0);
            else
                position = transform.position + new Vector3(2f * m_SpriteRenderer.sprite.bounds.extents.x + offsetX, offsetY - m_OffsetY, 0);

            GameObject tile = Instantiate(gameObject, position, transform.rotation);
            tile.transform.parent = transform.parent;
            Tiling tiling = tile.GetComponent<Tiling>();
            tiling.m_OffsetY = offsetY;

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


        /* OLD IMPLEMENTATION 
        // left
        if (!m_Left && Camera.main.transform.position.x - m_CameraWidth / 2f <= transform.position.x - m_SpriteRenderer.sprite.bounds.extents.x / 2f)
        {
            float offsetX = 0f;
            float offsetY = 0f;

            if (isRandomizedX)
                offsetX = -1f * Random.value;

            if (isRandomizedY)
                offsetY = 2f * (2f * Random.value - 1f);

            Vector3 position = transform.position + new Vector3(-2f * m_SpriteRenderer.sprite.bounds.extents.x + offsetX, offsetY - m_OffsetY, 0);

            GameObject tile = Instantiate(gameObject, position, transform.rotation);
            tile.transform.localScale = Vector3.Reflect(tile.transform.localScale, Vector3.left);
            tile.transform.parent = transform.parent;

            Tiling tiling = tile.GetComponent<Tiling>();
            
            m_Left = true;
            tiling.m_Right = true;
            tiling.m_OffsetY = offsetY;

        }
        // right
        else if (!m_Right && Camera.main.transform.position.x + m_CameraWidth / 2f >= transform.position.x + m_SpriteRenderer.sprite.bounds.extents.x / 2f)
        {
            float offsetX = 0f;
            float offsetY = 0f;

            if (isRandomizedX)
                offsetX = Random.value;

            if (isRandomizedY)
                offsetY = 2f * (2f * Random.value - 1f);

            Vector3 position = transform.position + new Vector3(2f * m_SpriteRenderer.sprite.bounds.extents.x + offsetX, offsetY - m_OffsetY, 0);

            GameObject tile = Instantiate(gameObject, position, transform.rotation);
            tile.transform.localScale = Vector3.Reflect(tile.transform.localScale, Vector3.left);
            tile.transform.parent = transform.parent;

            Tiling tiling = tile.GetComponent<Tiling>();

            m_Right = true;
            tiling.m_Left = true;
            tiling.m_OffsetY = offsetY;
        }*/
    }


    private void AssignRandomSprite()
    {
        m_SpriteRenderer.sprite =  m_Sprites[(int)((Random.value * m_Sprites.Length) % m_Sprites.Length)];
    }
}
