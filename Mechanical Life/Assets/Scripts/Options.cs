using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider MasterVolSlider;
    public Text VolumeSliderDisplay;
    public Slider DifficultySlider;
    public Text DifficultySliderDisplay;
    public Text MinText;
    public Text MaxText;

    // Use this for initialization
    void Awake()
    {
    }
    void Start()
    {
        MasterVolSlider.value = PlayerPrefsManager.GetMasterVolume();
        DifficultySlider.value = PlayerPrefsManager.GetDifficulty();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackToMenu()
    {
        MusicPlayer._instance.SetMasterVolume(PlayerPrefsManager.GetMasterVolume());
        ScreenManager._instance.LoadScreen("StartScreen");
    }

    public void UpdateVolumeDisplay()
    {
        VolumeSliderDisplay.text = (MasterVolSlider.value * 100).ToString("F0");
        MusicPlayer._instance.SetMasterVolume(MasterVolSlider.value);
    }

    public void UpdateDifficultyDisplay()
    {
        DifficultySliderDisplay.text = DifficultySlider.value.ToString();
        if (DifficultySlider.value == 1)
        {
            MinText.gameObject.SetActive(true);
			DifficultySliderDisplay.gameObject.SetActive(false);
        }
        else if (DifficultySlider.value == 10)
        {
            MaxText.gameObject.SetActive(true);
			DifficultySliderDisplay.gameObject.SetActive(false);
        }
        else
        {
			DifficultySliderDisplay.gameObject.SetActive(true);
            MinText.gameObject.SetActive(false);
            MaxText.gameObject.SetActive(false);
        }
    }

    public void SaveUserPrefs()
    {
        PlayerPrefsManager.SetMasterVolume(MasterVolSlider.value);
        PlayerPrefsManager.SetDifficulty(DifficultySlider.value);
    }
}
