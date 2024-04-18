using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Sounds
{
    public class GameSoundManager : MonoBehaviour
    {
        public static GameSoundManager Instance;

        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource effectsAudioSource;

        [SerializeField] private AudioClip music;
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip reboundSound;
        [SerializeField] private AudioClip enemyDieSound;

        private void Awake()
        {
            Instance = this;

            SettingsManager.OnSoundVolumeChanged += OnSoundVolumeChanged;
            SettingsManager.OnMusicVolumeChanged += OnMusicVolumeChanged;
            SettingsManager.OnMuteChanged += (mute) => {
                OnSoundVolumeChanged(SettingsManager.SoundVolume);
                OnMusicVolumeChanged(SettingsManager.MusicVolume);
            };
            OnSoundVolumeChanged(SettingsManager.SoundVolume);
            OnMusicVolumeChanged(SettingsManager.MusicVolume);

            PlayMusic();
        }

        private void OnDestroy()
        {
            SettingsManager.OnSoundVolumeChanged -= OnSoundVolumeChanged;
            SettingsManager.OnMusicVolumeChanged -= OnMusicVolumeChanged;
        }

        public void SetSoundVolume(float value) => 
            SettingsManager.SoundVolume = value;
        public void SetMusicVolume(float value) => 
            SettingsManager.MusicVolume = value;

        private void OnSoundVolumeChanged(float value) => 
            effectsAudioSource.volume = SettingsManager.Mute ? 0 : value;
        private void OnMusicVolumeChanged(float value) =>
            musicAudioSource.volume = SettingsManager.Mute ? 0 : value;

        public void PlayMusic()
        {
            musicAudioSource.clip = music;
            musicAudioSource.Play();
        }

        public void PlayEffect(AudioClip effectClip)
        {
            effectsAudioSource.PlayOneShot(effectClip);
        }

        public void PlayAttack() => PlayEffect(attackSound);
        public void PlayRebound() => PlayEffect(reboundSound);
        public void PlayEnemyDie() => PlayEffect(enemyDieSound);
    }
}