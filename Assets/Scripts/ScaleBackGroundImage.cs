using UnityEngine;
using UnityEngine.UI;


public class ScaleBackGroundImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        image.rectTransform.localScale = new Vector3(Screen.width / image.rectTransform.rect.width, Screen.height / image.rectTransform.rect.height, image.rectTransform.localScale.z);
    }
}
