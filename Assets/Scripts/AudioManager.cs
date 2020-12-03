using UnityEngine;




public class AudioManager : MonoBehaviour
{
    public Sound[] m_Sounds;




    private void Awake()
    {
        for (int i = 0; i < m_Sounds.Length; i++)
        {
            GameObject tmp = new GameObject();
            tmp.transform.parent = transform;
            m_Sounds[i].Set(tmp.AddComponent<AudioSource>());
        }
    }



    public void Play(string name)
    {
        for (int i = 0; i < m_Sounds.Length; i++)
            if (m_Sounds[i].m_Name == name)
            {
                m_Sounds[i].Play();
                return;
            }
    }
}
