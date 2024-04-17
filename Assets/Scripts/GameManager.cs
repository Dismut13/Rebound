using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private LastLineDrawer lastLineDrawer;

        [SerializeField] private Transform[] confetti;
        private List<string> levelScenes = new();

        private void Awake()
        {
            Instance = this;

            foreach(var scene in EditorBuildSettings.scenes)
                if (scene.path.Contains("Level"))
                    levelScenes.Add(scene.path.Split('/')[^1].Split('.')[0]);
        }

        public void Defeat(bool drawLastLine = true)
        {
            if (!drawLastLine)
                lastLineDrawer.Clear();
            StartCoroutine(ExecuteAfterTime(1, Restar));
        }

        public void Win()
        {
            LaunchConfetti();
            StartCoroutine(ExecuteAfterTime(2, ToNextLevel));
        }

        private void LaunchConfetti()
        {
            foreach (var obj in confetti)
                obj.gameObject.SetActive(true);
        }

        private IEnumerator ExecuteAfterTime(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }

        private void ToNextLevel()
        {
            var nextLevelIndex = int.Parse(SceneManager.GetActiveScene().name.Split(' ')[^1]) + 1;
            string nextLevelScene = levelScenes.First(s => s == $"Level 1");
            foreach (var scene in levelScenes)
                if (scene == $"Level {nextLevelIndex}")
                    nextLevelScene = scene;

            SceneManager.LoadScene(nextLevelScene);
        }

        public void Restar()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }

        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadMenu() => LoadScene("MainMenu");
        public void LoadShop() => LoadScene("Shop");
    }
}