using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastLineDrawer : MonoBehaviour
{
    [SerializeField] private LineFactory lineFactory;
    [SerializeField] private LastLineSaver saver;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform lineParent;

    private void Awake()
    {
        if (saver.LastScene == SceneManager.GetActiveScene().name)
            DrawLines(saver.Lines);
        else
            saver.LastScene = SceneManager.GetActiveScene().name;
        saver.Clear();

        lineFactory.OnLineCreated += () => saver.AddLine(new());
        lineFactory.OnPointAdded += point => saver.AddPoint(point);
    }

    private void DrawLines(List<List<Vector2>> lines)
    {
        foreach(var points in lines)
        {
            var line = CreateNewLine();
            foreach (var point in points)
                line.AddPoint(point);
        }
    }

    private Line CreateNewLine()
    {
        var currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity).GetComponent<Line>();
        currentLine.name = "Line";
        currentLine.transform.SetParent(lineParent);
        currentLine.SetRigidBodyType(RigidbodyType2D.Kinematic);
        currentLine.maxPoints = int.MaxValue;

        return currentLine;
    }
}
