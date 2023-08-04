using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class CircleGame : MonoBehaviour
{
    public GameObject circlePrefab;

    public int circleCount = 15;

    public float maxRadius = 2f;

    public float minDistance = 0.1f;

    public Canvas canvas;

    public GameObject restartButtonPrefab;

    private LineRenderer lineRenderer;

    private List<GameObject> circles;

    private List<Vector3> linePoints;

    private GameObject restartButton;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.loop = false;

        linePoints = new List<Vector3>();

        SpawnCircles();



    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0) || Input.touchCount > 0)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                linePoints.Clear();

                linePoints.Add(position);

                UpdateLineRenderer();
            }
            else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                linePoints.Add(position);

                UpdateLineRenderer();

                CreateRestartButton();
                CheckCircles();

            }
        }

        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            if (position != linePoints[linePoints.Count - 1])
            {
                linePoints.Add(position);

                UpdateLineRenderer();
            }
        }
    }

    void SpawnCircles()
    {
        circles = new List<GameObject>();

        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        Vector3 cameraMin = Camera.main.transform.position - new Vector3(cameraWidth / 2, cameraHeight / 2, 0);
        Vector3 cameraMax = Camera.main.transform.position + new Vector3(cameraWidth / 2, cameraHeight / 2, 0);

        int tries = 100;
        while (circles.Count < circleCount && tries > 0)
        {
            tries--;

            Vector3 position = new Vector3(Random.Range(cameraMin.x, cameraMax.x), Random.Range(cameraMin.y, cameraMax.y), 0);

            float radius = Random.Range(0.1f, maxRadius);

            bool overlap = false;
            foreach (GameObject circle in circles)
            {
                Vector3 otherPosition = circle.transform.position;
                float otherRadius = circle.transform.localScale.x / 2;

                float distance = Vector3.Distance(position, otherPosition);

                if (distance < radius + otherRadius + minDistance)
                {
                    overlap = true;
                    break;
                }
            }

            if (!overlap)
            {
                GameObject circle = Instantiate(circlePrefab, position, Quaternion.identity);
                circle.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

                circle.transform.localScale = new Vector3(radius * 2, radius * 2, 1);

                circles.Add(circle);
            }
        }
    }

    void CreateRestartButton()
    {
        restartButton = Instantiate(restartButtonPrefab, canvas.transform);

        Button button = restartButton.GetComponent<Button>();
        button.onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        foreach (GameObject circle in circles)
        {
            Destroy(circle);
        }

        circles.Clear();

        SpawnCircles();

        linePoints.Clear();



        lineRenderer.positionCount = 0;
    }

    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = linePoints.Count;

        for (int i = 0; i < linePoints.Count; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i]);
        }
    }

    void CheckCircles()
    {
        foreach (GameObject circle in circles)
        {
            Vector3 position = circle.transform.position;
            float radius = circle.transform.localScale.x / 2;

            for (int i = 0; i < linePoints.Count - 1; i++)
            {
                Vector3 start = linePoints[i];
                Vector3 end = linePoints[i + 1];

                float distance = DistancePointSegment(position, start, end);

                if (distance <= radius)
                {
                    circle.SetActive(false);
                    break;
                }
            }
        }
    }

    float DistancePointSegment(Vector3 point, Vector3 start, Vector3 end)
    {
        Vector3 segment = end - start;
        float segmentSqrMag = segment.sqrMagnitude;

        if (segmentSqrMag == 0)
        {
            return Vector3.Distance(point, start);
        }

        float t = Mathf.Clamp01(Vector3.Dot(point - start, segment) / segmentSqrMag);

        Vector3 closest = start + t * segment;

        return Vector3.Distance(point, closest);
    }
}