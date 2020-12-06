using UnityEngine;
using UnityEngine.Assertions;
using TMPro;




public class AchievementManager : MonoBehaviour
{
    [System.Serializable] public struct Achievement
    {
        public string name;
        public string description;
    }




    private static AchievementManager m_Instance;

    [SerializeField] private Achievement[] m_Achievements;

    [SerializeField] private const string prefix = "ach_";

    [SerializeField] private Transform m_Popup;
    private Animator m_Animator;
    private TextMeshProUGUI m_Text;




    private void Awake()
    {
        if (m_Instance != null)
        {
            if (m_Instance != this)
                Destroy(this);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(this);

            for (int i = 0; i < m_Achievements.Length; i++)
                if (!PlayerPrefs.HasKey(prefix + m_Achievements[i].name))
                    PlayerPrefs.SetInt(prefix + m_Achievements[i].name, 0);

            EventSystem.current.OnEnemyKill += EnemyKilled;
        }
    }



    private void Start()
    {
        Assert.IsNotNull(m_Popup);
        m_Animator = m_Popup.GetComponent<Animator>();
        m_Text = m_Popup.GetComponentsInChildren<TextMeshProUGUI>()[1];
    }



    private void OnDestroy() { EventSystem.current.OnEnemyKill -= EnemyKilled; }



    public void AchievementAchieved(string name)
    {
        for (int i = 0; i < m_Achievements.Length; i++)
            if (m_Achievements[i].name == name)
            {
                if (PlayerPrefs.GetInt(prefix + name) == 0)
                {
                    m_Text.text = name;
                    m_Animator.SetTrigger("Pop");
                    PlayerPrefs.SetInt(prefix + name, 1);
                }

                return;
            }
                

        Debug.LogError("NO ACHIEVEMENT WITH NAME \"" + name + "\"");
    }


    public void EnemyKilled() { AchievementAchieved("First Blood"); }
}
