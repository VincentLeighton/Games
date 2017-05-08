using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager _instance;

    public float AutoLoadNextLevelDelay = 3f;
    public FaderPanel fader;
    public List<Attacker> Attackers;

    public static ScreenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Trying to accesss null reference of Instance.");
            }
            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        if (AutoLoadNextLevelDelay > 0)
        {
            Invoke("LoadNextScreen", AutoLoadNextLevelDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScreen(string ScreenName)
    {
        SceneManager.LoadScene(ScreenName, LoadSceneMode.Single);
    }

    public void LoadNextScreen()
    {
        Scene ActiveScene = SceneManager.GetActiveScene();
        int Index = ActiveScene.buildIndex;
        //Debug.LogFormat ("Index {0}, Total - 2:{1}", Index, SceneManager.sceneCountInBuildSettings - 2);
        if (Index == SceneManager.sceneCountInBuildSettings - 2)
        {
            SceneManager.LoadScene("VictoryScreen");
        }
        else
        {
            //Debug.LogFormat ("Loading Scene:{0}", Index + 1);
            SceneManager.LoadScene(Index + 1);
        }
    }

    public void AddAttacker(Attacker a)
    {
        Attackers.Add(a);
    }

}
