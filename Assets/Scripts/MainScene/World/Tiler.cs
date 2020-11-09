using System;
using UnityEngine;


public class Tiler : MonoBehaviour
{
    public TilingLayer[] m_TilingLayers;

    private float s_CameraWidth;


    // Start is called before the first frame update
    void Start()
    {
        s_CameraWidth = Camera.main.ViewportToWorldPoint(Vector3.one).x - Camera.main.ViewportToWorldPoint(Vector3.zero).x;
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_TilingLayers.Length; i++)
        {
            float leftSpriteExtentX = m_TilingLayers[i].leftEdge.GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
            float rightSpriteExtentX = m_TilingLayers[i].rightEdge.GetComponent<SpriteRenderer>().sprite.bounds.extents.x;

            bool leftSpawn = Camera.main.transform.position.x - s_CameraWidth / 2f <= m_TilingLayers[i].leftEdge.transform.position.x - leftSpriteExtentX / 2f;
            bool rightSpawn = Camera.main.transform.position.x + s_CameraWidth / 2f >= m_TilingLayers[i].rightEdge.transform.position.x + rightSpriteExtentX / 2f;

            while(leftSpawn || rightSpawn)
            {
                if (leftSpawn)
                {
                    float offsetX = 0f;
                    float offsetY = 0f;

                    if (m_TilingLayers[i].isRandomizedX)
                        offsetX = m_TilingLayers[i].maxSpaceX * UnityEngine.Random.value;

                    if (m_TilingLayers[i].isRandomizedY)
                        offsetY = 2f * (m_TilingLayers[i].maxSpaceY * UnityEngine.Random.value - m_TilingLayers[i].maxSpaceY / 2f);

                    Vector3 position = m_TilingLayers[i].leftEdge.transform.position + new Vector3(-2f * leftSpriteExtentX - offsetX, offsetY - m_TilingLayers[i].GetLeftOffsetY(), 0);

                    GameObject tile = Instantiate(m_TilingLayers[i].prototype, position, m_TilingLayers[i].rightEdge.transform.rotation);
                    tile.transform.parent = m_TilingLayers[i].leftEdge.transform.parent;

                    SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = m_TilingLayers[i].sprites[Mathf.RoundToInt(UnityEngine.Random.value * m_TilingLayers[i].sprites.Length) % m_TilingLayers[i].sprites.Length];

                    tile.SetActive(true);

                    m_TilingLayers[i].leftEdge = tile;
                    m_TilingLayers[i].SetLeftOffsetY(offsetY);

                    leftSpriteExtentX = spriteRenderer.sprite.bounds.extents.x;
                    leftSpawn = Camera.main.transform.position.x - s_CameraWidth / 2f <= tile.transform.position.x - leftSpriteExtentX / 2f;
                }

                if (rightSpawn)
                {
                    float offsetX = 0f;
                    float offsetY = 0f;

                    if (m_TilingLayers[i].isRandomizedX)
                        offsetX = m_TilingLayers[i].maxSpaceX * UnityEngine.Random.value;

                    if (m_TilingLayers[i].isRandomizedY)
                        offsetY = 2f * (m_TilingLayers[i].maxSpaceY * UnityEngine.Random.value - m_TilingLayers[i].maxSpaceY / 2f);

                    Vector3 position = m_TilingLayers[i].rightEdge.transform.position + new Vector3(2f * rightSpriteExtentX + offsetX, offsetY - m_TilingLayers[i].GetRightOffsetY(), 0);

                    GameObject tile = Instantiate(m_TilingLayers[i].prototype, position, m_TilingLayers[i].rightEdge.transform.rotation);
                    tile.transform.parent = m_TilingLayers[i].rightEdge.transform.parent;

                    SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = m_TilingLayers[i].sprites[Mathf.RoundToInt(UnityEngine.Random.value * m_TilingLayers[i].sprites.Length) % m_TilingLayers[i].sprites.Length];

                    tile.SetActive(true);

                    m_TilingLayers[i].rightEdge = tile;
                    m_TilingLayers[i].SetRightOffsetY(offsetY);

                    rightSpriteExtentX = spriteRenderer.sprite.bounds.extents.x;
                    rightSpawn = Camera.main.transform.position.x + s_CameraWidth / 2f >= tile.transform.position.x + rightSpriteExtentX / 2f;
                }
            }
        }
    }


    public TilingLayer GetLayer(int index) { return m_TilingLayers[index]; }

    public void SetLayer(int index, TilingLayer layer)
    {
        if (index < m_TilingLayers.Length)
            m_TilingLayers[index] = layer;
    }


    [Serializable]
    public struct TilingLayer
    {
        public GameObject prototype;

        public GameObject leftEdge;
        public GameObject rightEdge;

        public bool isRandomizedX;
        public float maxSpaceX;

        public bool isRandomizedY;
        public float maxSpaceY;

        public Sprite[] sprites;

        private float leftOffsetY;
        private float rightOffsetY;

        public float GetLeftOffsetY() { return leftOffsetY; }
        public void SetLeftOffsetY(float offset) { leftOffsetY = offset; }

        public float GetRightOffsetY() { return rightOffsetY; }
        public void SetRightOffsetY(float offset) { rightOffsetY = offset; }
    }
}