using UnityEngine;



[System.Serializable]
public struct Sound
{
    public string m_Name;
    public AudioClip m_Clip;
    public AudioSource m_Source;
    [Range(0, 1)] public float m_Volume;
    [Range(0, 10)] public float m_Pitch;
    [Range(0, 0.5f)] public float m_VolumeRandomness;
    [Range(0, 0.5f)] public float m_PitchRandomness;
    public bool m_isLooping;



    public void Set(AudioSource source)
    {
        m_Source = source;
        m_Source.volume = m_Volume;
        m_Source.pitch = m_Pitch;
        m_Source.clip = m_Clip;
        m_Source.loop = m_isLooping;
    }



    public void Play()
    {
        m_Source.volume = m_Volume + (Random.Range(-m_VolumeRandomness, m_VolumeRandomness));
        m_Source.pitch = m_Pitch + (Random.Range(-m_PitchRandomness, m_PitchRandomness));
        m_Source.Play();
    }
}
