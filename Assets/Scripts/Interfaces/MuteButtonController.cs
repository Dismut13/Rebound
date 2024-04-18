using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interfaces
{
    public class MuteButtonController : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite volumeOnSprite;
        [SerializeField] private Sprite volumeOffSprite;

        private void Awake()
        {
            Redraw();
        }

        public void OnClick()
        {
            SettingsManager.Mute = !SettingsManager.Mute;
            Redraw();
        }

        private void Redraw()
        {
            image.sprite = SettingsManager.Mute ?
                volumeOffSprite :
                volumeOnSprite;
        }
    }
}