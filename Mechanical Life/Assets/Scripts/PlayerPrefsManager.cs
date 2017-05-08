using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerPrefsManager : MonoBehaviour
{

    public const string MASTER_VOLUME_KEY = "master_volume";
    const float MAX_VOLUME = 1f;
    const float MIN_VOLUME = 0f;
    const string DIFFICULTY = "difficulty";
    const float MIN_DIFFICULTY = 1;
    const float MAX_DIFFICULTY = 10;
    const string LEVEL_KEY = "level_unlocked_";
    const string MAX_LEVEL_KEY = "max_level";
    const int MAX_LEVEL = 1;
    const int MIN_LEVEL = 0;
    const int LOCKED = 0;
    const int UNLOCKED = 1;
    private static Dictionary<int, bool> _levels;
    // Use this for initialization
    public static void SetMasterVolume(float newVolume)
    {

        if (newVolume > MAX_VOLUME)
        {
            newVolume = MAX_VOLUME;
        }
        else if (newVolume < MIN_VOLUME)
        {
            newVolume = MIN_VOLUME;
        }
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, newVolume);
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, MAX_VOLUME);
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY, MIN_DIFFICULTY);
    }
    public static void SetDifficulty(float newDifficulty)
    {
        if (newDifficulty >= MIN_DIFFICULTY && newDifficulty <= MAX_DIFFICULTY)
        {
            PlayerPrefs.SetFloat(DIFFICULTY, newDifficulty);
        }
    }

    public static void UnlockLevel(int intendedLevel)
    {
        if (intendedLevel < MIN_LEVEL)
        {
            intendedLevel = MIN_LEVEL;
        }
        else if (intendedLevel > MAX_LEVEL)
        {
            intendedLevel = MAX_LEVEL;
        }
        string levelLookupKey = string.Format("{0}{1}", LEVEL_KEY, intendedLevel);
        //setting player prefs
        PlayerPrefs.SetInt(levelLookupKey, UNLOCKED);
        //updating cache of levels
        if (_levels != null)
        {
            _levels[intendedLevel] = true;
        }
    }

    public static Dictionary<int, bool> GetUnlockedLevels(bool refresh)
    {
        if (!refresh && _levels != null)
        {
            return _levels;
        }
        Dictionary<int, bool> levels = new Dictionary<int, bool>();
        //loop over all levels
        //check if level is set 0 (locked) or 1(unlocked)
        for (int i = 0; i <= MAX_LEVEL; i++)
        {
            levels.Add(i, IsLevelUnlocked(i));
        }
        //return unlocked levels delimeted by whitespace
        _levels = levels;
        return _levels;
    }

    public static bool IsLevelUnlocked(int levelInQuestion)
    {
        if (_levels != null)
        {
            return _levels[levelInQuestion];
        }
        bool isUnlocked = false;
        //check if LEVEL_KEY_levelInQuestion = 0 || 1
        if (PlayerPrefs.GetInt(string.Format("{0}{1}", LEVEL_KEY, levelInQuestion)) == UNLOCKED)
        {
            isUnlocked = true;
        }
        return isUnlocked;
    }

    public static void PrintLevels()
    {
        GetUnlockedLevels(true);
        foreach (KeyValuePair<int, bool> pair in _levels)
        {
            //Debug.LogFormat("Level:{0} Status:{1}", pair.Key, pair.Value);
        }
    }
}
