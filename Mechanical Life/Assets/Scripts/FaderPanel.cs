using UnityEngine;
using UnityEngine.UI;

public class FaderPanel : MonoBehaviour
{
    Image image;
    public float FadeInTime;
    public float FadeOutTime;
    Color currentColor;
    float alphaChgPerFrame;


    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        currentColor = image.color;
        alphaChgPerFrame = Time.deltaTime / FadeInTime;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //
        if (FadeInTime > 0)
        {
            FadeIn();
        }
        else if (FadeOutTime > 0)
        {
            FadeOut();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void FadeIn()
    {
        if (Time.timeSinceLevelLoad < FadeInTime)
        {
            currentColor.a -= alphaChgPerFrame;
            image.color = currentColor;
        }
        else
        {
			FadeInTime = 0;
        }
    }

    public void FadeOut()
    {
        if (Time.timeSinceLevelLoad > FadeInTime)
        {
            currentColor.a += alphaChgPerFrame;
            image.color = currentColor;
        }
        else
        {
			FadeOutTime = 0;
        }
    }
}
