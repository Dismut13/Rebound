using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class LineFactory : MonoBehaviour
{
    public event System.Action LineReleased;
    public event System.Action<float> LineProgressChanging;
    public GameObject linePrefab;
    [HideInInspector]
    public Line currentLine;
    public Transform lineParent;
    public RigidbodyType2D lineRigidBodyType = RigidbodyType2D.Kinematic;
    public LineEnableMode lineEnableMode = LineEnableMode.ON_CREATE;
    public static LineFactory instance;
    public Image lineLife;
    public bool enableLineLife;
    public bool isRunning;
    [SerializeField] private LayerMask _notDrawableLayer;
    [SerializeField] private float _maxLinePoints = 100;
    private bool _lineCreated;
    public GameObject Dline;
    public GameObject DlineTwo;
    public GameObject DlineThree;
    public GameObject DlineFour;

    public GameObject LoPanel;
    [SerializeField] private float delayTime = 5f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0;

        if (lineParent == null)
        {
            lineParent = GameObject.Find("Lines").transform;
        }

        if (lineLife != null)
        {
            if (enableLineLife)
            {
                lineLife.gameObject.SetActive(true);
            }
            else
            {
                lineLife.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !_lineCreated)
        {
            CreateNewLine();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            RelaseCurrentLine();
            Time.timeScale = 1;
            Dline.GetComponent<Collider2D>().enabled = false;
            DlineTwo.GetComponent<Collider2D>().enabled = false;
            DlineThree.GetComponent<Collider2D>().enabled = false;
            DlineFour.GetComponent<Collider2D>().enabled = false;
            StartCoroutine("Tim");
        }

        if (currentLine != null && !_lineCreated)
        {
            if (currentLine.points.Count > _maxLinePoints) return;
            //проверка на зону препятствий
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, 100f, _notDrawableLayer);
            if (!hit)
            {
                var lastPoint = currentLine.points.Count > 0 ?
                    currentLine.points[currentLine.points.Count - 1] : mousePosition;
                Vector2 rayDirection = mousePosition - lastPoint;
                if (Physics2D.Raycast(lastPoint, rayDirection.normalized,
                    rayDirection.magnitude, _notDrawableLayer)) return;
                currentLine.AddPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                UpdateLineLife();
                if (currentLine.ReachedPointsLimit())
                {
                    RelaseCurrentLine();
                }
            }

        }
    }

    private void CreateNewLine()
    {
        currentLine = (Instantiate(linePrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Line>();
        currentLine.name = "Line";
        currentLine.transform.SetParent(lineParent);
        currentLine.SetRigidBodyType(lineRigidBodyType);
        currentLine.maxPoints = _maxLinePoints;

        if (lineEnableMode == LineEnableMode.ON_CREATE)
        {
            EnableLine();
        }
    }

    private void EnableLine()
    {
        currentLine.EnableCollider();
        currentLine.SimulateRigidBody();
    }

    private void RelaseCurrentLine()
    {
        if (lineEnableMode == LineEnableMode.ON_RELASE && !_lineCreated)
        {
            EnableLine();
            LineReleased?.Invoke();
            print("LINE RELEASED");
            _lineCreated = true;
        }
    }

    private void UpdateLineLife()
    {
        if (!enableLineLife)
        {
            return;
        }
        var lineProgress = 1 - (currentLine.points.Count / currentLine.maxPoints);
        LineProgressChanging?.Invoke(lineProgress);
    }

    IEnumerator Tim()
    {
        yield return new WaitForSeconds(delayTime);
        Time.timeScale = 0;
        LoPanel.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    public enum LineEnableMode
    {
        ON_CREATE,
        ON_RELASE
    }

    ;
}
