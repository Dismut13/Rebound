using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "HintLineSaver", menuName = "Custom/HintLineSaver", order = 1)]
public class HintLineSaver : ScriptableObject
{
    [SerializeField] private List<LevelHintLine> levelHintLines;
    [SerializeField] private LastLineSaver saver;

    public void AddLevelHintLines(int levelIndex, List<NestedList<Vector2>> lines)
    {
        levelHintLines.RemoveAll(lhl => lhl.LevelInex == levelIndex);
        levelHintLines.Add(new LevelHintLine(levelIndex, lines));
    }

    public List<NestedList<Vector2>> GetLevelHintLines(int levelIndex) =>
        levelHintLines.Find(lhl => lhl.LevelInex == levelIndex).Lines;

    public List<NestedList<Vector2>> GetCurrentLevelHintLines() =>
        levelHintLines.Find(lhl => lhl.LevelInex == GetCurrentLevelIndex())?.Lines;

    private int GetCurrentLevelIndex() => 
        int.Parse(SceneManager.GetActiveScene().name.Split(' ')[^1]);

    public void SetSavedLine()
    {
        AddLevelHintLines(
            GetCurrentLevelIndex(), 
            saver.Lines.Select(line => new NestedList<Vector2> { List = line }).ToList()
        );
    }
}

[System.Serializable]
class LevelHintLine
{
    public int LevelInex => levelIndex;
    [SerializeField] private int levelIndex;

    public List<NestedList<Vector2>> Lines => lines;
    [SerializeField] private List<NestedList<Vector2>> lines;

    public LevelHintLine(int levelIndex, List<NestedList<Vector2>> lines)
    {
        this.levelIndex = levelIndex;
        this.lines = lines;
    }
}

[System.Serializable]
public class NestedList<T>
{
    public List<T> List;
}

#if UNITY_EDITOR

[CustomEditor(typeof(HintLineSaver))]
public class HintLineSaverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Save current line"))
        {
            (target as HintLineSaver).SetSavedLine();
        }
    }
}

#endif
