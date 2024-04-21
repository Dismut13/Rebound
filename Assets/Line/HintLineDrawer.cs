using UnityEngine;

public class HintLineDrawer : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform lineParent;
    [SerializeField] private HintLineSaver saver;

    public void DrawLines()
    {
        var lines = saver.GetCurrentLevelHintLines();
        if (lines != null)
            foreach (var points in saver.GetCurrentLevelHintLines())
            {
                var line = CreateNewLine();
                foreach (var point in points.List)
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


