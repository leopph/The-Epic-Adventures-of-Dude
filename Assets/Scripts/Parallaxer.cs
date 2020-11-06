using UnityEngine;
using System;


public class Parallaxer : MonoBehaviour
{
    private float m_PrevCamPos;

    [Serializable]
    public struct ElementLayer
    {
        /* Depth level is a float between 1 and -1.
        * Negative values are farther from the camera */
        public float DepthLevel;
        public Transform Layer;
    }

    public ElementLayer[] m_Layers;


    // Start is called before the first frame update
    void Start()
    {
        m_PrevCamPos = Camera.main.transform.position.x;
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_Layers.Length; i++)
            m_Layers[i].Layer.position += new Vector3((Camera.main.transform.position.x - m_PrevCamPos) * -1 * m_Layers[i].DepthLevel, 0, 0);

        m_PrevCamPos = Camera.main.transform.position.x;
    }
}
