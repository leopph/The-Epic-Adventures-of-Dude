using UnityEngine;




public class AudioManager : MonoBehaviour
{
    public Sound[] m_Sounds;
    public Sound[] m_Music;




    private void Awake()
    {
        for (int i = 0; i < m_Sounds.Length; i++)
        {
            GameObject tmp = new GameObject();
            tmp.transform.parent = transform;
            m_Sounds[i].Set(tmp.AddComponent<AudioSource>());
        }

        for (int i = 0; i < m_Music.Length; i++)
        {
            GameObject tmp = new GameObject();
            tmp.transform.parent = transform;
            m_Music[i].Set(tmp.AddComponent<AudioSource>());
        }
    }



    public void PlaySound(string name)
    {
        for (int i = 0; i < m_Sounds.Length; i++)
            if (m_Sounds[i].m_Name == name)
            {
                m_Sounds[i].Play();
                return;
            }

        Debug.LogError("THERE IS NO SOUND WITH NAME \"" + name + "\"!");
    }


    public void PlayMusic(string name)
    {
        for (int i = 0; i < m_Music.Length; i++)
            if (m_Music[i].m_Name == name)
            {
                m_Music[i].Play();
                return;
            }

        Debug.LogError("THERE IS NO MUSIC WITH NAME \"" + name + "\"!");
    }



    public void PauseSounds()
    {
        for (int i = 0; i < m_Sounds.Length; i++)
                m_Sounds[i].Pause();
    }



    public void ResumeSounds()
    {
        for (int i = 0; i < m_Sounds.Length; i++)
                m_Sounds[i].Resume();
    }



    public void PauseMusic(string name)
    {
        for (int i = 0; i < m_Music.Length; i++)
            if (m_Music[i].m_Name == name)
            {
                m_Music[i].Pause();
                return;
            }

        Debug.LogError("THERE IS NO MUSIC WITH NAME \"" + name + "\"!");
    }



    public void ResumeMusic(string name)
    {
        for (int i = 0; i < m_Music.Length; i++)
            if (m_Music[i].m_Name == name)
            {
                m_Music[i].Resume();
                return;
            }

        Debug.LogError("THERE IS NO MUSIC WITH NAME \"" + name + "\"!");
    }


    public void StopMusic(string name)
    {
        for (int i = 0; i < m_Music.Length; i++)
            if (m_Music[i].m_Name == name)
            {
                m_Music[i].Stop();
                return;
            }

        Debug.LogError("THERE IS NO MUSIC WITH NAME \"" + name + "\"!");
    }
}
