using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Interfaces
{
    public class PlayButtonController : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene($"Level {1}");
        }
    }
}