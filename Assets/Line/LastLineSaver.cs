using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LastLineSaver", menuName = "Custom/LastLineSaver", order = 1)]
public class LastLineSaver : ScriptableObject
{
    private string lastScene;
    public string LastScene { get => lastScene; set => lastScene = value; }

    private List<List<Vector2>> lines = new();
    public List<List<Vector2>> Lines => lines;
        
    public void AddLine(List<Vector2> line)
    {
        lines.Add(line);
    }

    public void AddPoint(Vector2 point)
    {
        lines[^1].Add(point);
    }

    public void Clear()
    {
        lines.Clear();
    }
}
