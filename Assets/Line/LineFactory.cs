using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class LineFactory : MonoBehaviour
{
    public static LineFactory Instance { get; private set; }

    public event Action OnLineCreated;
    public event Action OnLineReleased;
    public event Action<float> OnLineProgressChanged;
    public event Action<Vector2> OnPointAdded;

    [SerializeField] private GameObject linePrefab;
    [SerializeField, HideInInspector] private Line currentLine;
    [SerializeField] private Transform lineParent;
    [SerializeField] private RigidbodyType2D lineRigidBodyType = RigidbodyType2D.Kinematic;
    [SerializeField] private LineEnableMode lineEnableMode = LineEnableMode.OnCreate;
    [SerializeField] private bool isRunning;
    [SerializeField] private LineLayerAllowingMode layerMode;
    [SerializeField] private LayerMask allowedLayer;
    [SerializeField] private LayerMask disallowedLayer;
    [SerializeField] private float maxLinePoints = 100;
    [SerializeField] private float maxLineCount = 1;

    private int lineCount;

    void Awake()
    {
        Instance = this;

        lineParent ??= transform;
    }

    void Update()
    {
        if (!isRunning)
            return;

        if (Input.GetMouseButtonDown(0) && lineCount < maxLineCount)
            CreateNewLine();

        if (currentLine != null)
            CheckLineReleasing();
    }

    private void CheckLineReleasing()
    {
        if (Input.GetMouseButtonUp(0) || currentLine.ReachedPointsLimit())
        {
            RelaseCurrentLine();
            return;
        }

        if (IsLineAtLayer(disallowedLayer) || (layerMode == LineLayerAllowingMode.AllowedLayer && !IsLineAtLayer(allowedLayer)))
            return;

        UpdateLineLife();
    }

    private bool IsLineAtLayer(LayerMask zoneLayer)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mousePosHit = Physics2D.Raycast(mousePosition, Vector3.forward, 100f, zoneLayer);
        if (mousePosHit)
            return true;

        var lastPoint = currentLine.points.Count > 0 ?
                currentLine.points[currentLine.points.Count - 1] :
                mousePosition;
        Vector2 rayDirection = mousePosition - lastPoint;
        var toLastPointHit = Physics2D.Raycast(lastPoint, rayDirection.normalized, rayDirection.magnitude, zoneLayer);
        if (toLastPointHit)
            return true;

        return false;
    }

    private void CreateNewLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity).GetComponent<Line>();
        currentLine.name = "Line";
        currentLine.transform.SetParent(lineParent);
        currentLine.SetRigidBodyType(lineRigidBodyType);
        currentLine.maxPoints = maxLinePoints;

        if (lineEnableMode == LineEnableMode.OnCreate)
            EnableLine();

        OnLineCreated?.Invoke();
    }

    private void EnableLine()
    {
        currentLine.EnableCollider();
        currentLine.SimulateRigidBody();
    }

    private void RelaseCurrentLine()
    {
        if (lineEnableMode == LineEnableMode.OnRelease)
        {
            EnableLine();
            OnLineReleased?.Invoke();
            print("LINE RELEASED");
            lineCount++;
            currentLine = null;
        }
    }

    private void UpdateLineLife()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentLine.AddPoint(mousePosition);
        OnPointAdded?.Invoke(mousePosition);

        var lineProgress = 1 - (currentLine.points.Count / currentLine.maxPoints);
        OnLineProgressChanged?.Invoke(lineProgress);
    }

    public enum LineEnableMode
    {
        OnCreate,
        OnRelease
    }

    enum LineLayerAllowingMode
    {
        Ignore,
        AllowedLayer
    }
}
