using UnityEngine;


public class ParkourZone : TriggerZone
{
    public float m_Length;
    public int m_LayerIndex;
    public bool m_RandomizeX;
    public bool m_RandomizeY;
    public float m_MaxBlockDistanceX;
    public float m_MaxBlockDistanceY;

    private Tiler m_Tiler;
    private State m_State = State.NotInZone;
    private Tiler.TilingLayer m_Backup;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_Tiler = GameObject.Find("Tiler").GetComponent<Tiler>();
    }


    // Update is called once per frame
    void Update()
    {
        if (m_State != State.Done)
            switch (m_State)
            {
                case State.NotInZone:
                    if (m_Player.position.x >= transform.position.x)
                    {
                        m_State = State.InZone;

                        m_Backup = m_Tiler.GetLayer(m_LayerIndex);

                        Tiler.TilingLayer layer = m_Backup;
                        layer.isRandomizedX = m_RandomizeX;
                        layer.maxSpaceX = m_MaxBlockDistanceX;
                        layer.isRandomizedY = m_RandomizeY;
                        layer.maxSpaceY = m_MaxBlockDistanceY;

                        m_Tiler.SetLayer(m_LayerIndex, layer);

                        GameObject deathZone = new GameObject();
                        deathZone.transform.position = new Vector3(layer.rightEdge.transform.position.x, layer.rightEdge.transform.position.y - m_MaxBlockDistanceY - 2f, layer.rightEdge.transform.position.z);
                        deathZone.AddComponent<DeathZone>();
                        deathZone.AddComponent<EdgeCollider2D>();
                        EdgeCollider2D collider = deathZone.GetComponent<EdgeCollider2D>();
                        collider.points = new Vector2[]{ new Vector2(0, 0), new Vector2(m_Length + 2 * layer.rightEdge.GetComponent<SpriteRenderer>().sprite.bounds.extents.x, 0)};
                    }
                    break;

                case State.InZone:
                    if (m_Player.position.x > transform.position.x + m_Length)
                    {
                        m_State = State.Done;
                        m_Backup.rightEdge = m_Tiler.GetLayer(m_LayerIndex).rightEdge;
                        m_Backup.SetRightOffsetY(m_Tiler.GetLayer(m_LayerIndex).GetRightOffsetY());
                        m_Tiler.SetLayer(m_LayerIndex, m_Backup);
                    }
                    break;
            }
    }


    private enum State
    {
        NotInZone,
        InZone,
        Done
    }
}
