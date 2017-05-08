using UnityEngine;

public class VictoryScreen : MonoBehaviour
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
