using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public FaderPanel fader;
    // Use this for initialization
    void Start()
    {
        PlayerPrefsManager.UnlockLevel(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onStart()
    {
        ScreenManager.Instance.LoadNextScreen();
        MusicPlayer._instance.PlayGameOn();
    }

    public void Options(){
        ScreenManager.Instance.LoadScreen("Options");
    }

    public void LevelSelect(){
        ScreenManager.Instance.LoadScreen("LevelSelect");
    }
}
