using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject m_LoadingScreen;
    public Slider m_LoadingSlider;
    public GameObject m_ContinueText;


    private void OnEnable()
    {
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            m_LoadingSlider.value = asyncOperation.progress / 0.9f;

            if (asyncOperation.progress == 0.9f)
            {
                m_ContinueText.SetActive(true);

                if (Input.anyKey)
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
