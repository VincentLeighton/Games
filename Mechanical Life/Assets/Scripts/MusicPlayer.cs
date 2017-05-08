using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer _instance;
    public float MasterVolumeValue;
    // Use this for initialization
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

    void Start(){
        SetMasterVolume(PlayerPrefsManager.GetMasterVolume());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMasterVolume(float newVolume){
        MasterVolumeValue = newVolume;
        AudioSource source = gameObject.transform.FindChild("GameMusic").GetComponent<AudioSource>();
        source.volume = MasterVolumeValue;
    }

    public void PlayDeathSound()
    {
        Transform playerDeathSound = gameObject.transform.FindChild("PlayerDeath");
        AudioSource source = playerDeathSound.GetComponent<AudioSource>();
        source.Play();
    }

    public void PlayGameOn()
    {
        Transform GameOnSound = transform.Find("GameOnSound");
        AudioSource source = GameOnSound.GetComponent<AudioSource>();
        source.Play();
    }

    public void PlaySplashLoaded()
    {
        Transform GameOnSound = transform.Find("SplashSound");
        AudioSource source = GameOnSound.GetComponent<AudioSource>();
        source.Play();
    }
}
