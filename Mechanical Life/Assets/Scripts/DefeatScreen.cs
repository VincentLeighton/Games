using UnityEngine;

public class DefeatScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        ScreenManager.Instance.LoadScreen("StartScreen");
    }
}
