using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class SettingsManager
    {
        public static float SoundVolume { get => soundVolume; set { 
                SaveValue("soundVolume", value); 
                soundVolume = value;
                OnSoundVolumeChanged?.Invoke(value);
            } }
        private static float soundVolume;
        public static event Action<float> OnSoundVolumeChanged;

        public static float MusicVolume { get => musicVolume; set { 
                SaveValue("musicVolume", value); 
                musicVolume = value;
                OnMusicVolumeChanged?.Invoke(value);
            } }
        private static float musicVolume;
        public static event Action<float> OnMusicVolumeChanged;

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            soundVolume = LoadValue("soundVolume", 1f);
            musicVolume = LoadValue("musicVolume", 1f);
        }

        private static void SaveValue(string name, float value)
        {
            PlayerPrefs.SetFloat(name, value);
        }
        private static void SaveValue(string name, int value)
        {
            PlayerPrefs.SetInt(name, value);
        }
        private static void SaveValue(string name, string value)
        {
            PlayerPrefs.SetString(name, value);
        }

        private static float LoadValue(string name, float defaultValue = 0)
        {
            return PlayerPrefs.GetFloat(name, defaultValue);
        }

        private static int LoadValue(string name, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(name, defaultValue);
        }
    }
}
