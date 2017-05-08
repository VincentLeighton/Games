using UnityEngine;

public class Loader : MonoBehaviour
{

    public ScreenManager _ScreenManager;
    public MusicPlayer _MusicPlayer;
    public GameScreen _GameScreen;

    public void Awake()
    {
        if (ScreenManager._instance == null)
        {
            Instantiate(_ScreenManager);
        }

        if (MusicPlayer._instance == null)
        {
            Instantiate(_MusicPlayer);
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
