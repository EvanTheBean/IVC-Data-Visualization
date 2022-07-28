using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GraphPositionEditor : MonoBehaviour
{
    GameObject holder;
    Vector3 centerPoint;

    float rotationSpeed = 0.2f;
    bool freetransform = true;

    float arSize = 0.03f;
    LineRenderer[] lines;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        holder = transform.GetChild(0).gameObject;
        lines = GetComponentsInChildren<LineRenderer>();

        centerPoint = holder.GetComponent<Holder>().CalculateCenterPoint();
        transform.position = new Vector3(0, 0f, centerPoint.z*4);

        GraphsManager.Instance.ReceiveGraph(gameObject);
    }

    private void Update()
    {
        if (freetransform)
        {
            DoRotation();
            DoZoom();
        }
    }

    void DoRotation()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        if (Input.touchCount > 0)
        {
            XaxisRotation = Input.touches[0].deltaPosition.x * rotationSpeed;
            YaxisRotation = Input.touches[0].deltaPosition.y * rotationSpeed;
        }

        transform.Rotate(Vector3.down, XaxisRotation,Space.World);
        transform.Rotate(Vector3.right, YaxisRotation,Space.World);
    }

    Touch[] touches;

    float initialDistance;
    Vector3 initialScale;
    void DoZoom()
    {
        if (Input.touchCount >= 2)
        {
            touches = new Touch[Input.touchCount];
            for (int i = 0; i < Input.touchCount; i++)
            {
                touches[i] = Input.GetTouch(i);
            }

            Vector2[] positions = new Vector2[Input.touchCount];
            for (int i = 0; i < touches.Length; i++)
            {
                if (touches[i].phase == TouchPhase.Ended || touches[i].phase == TouchPhase.Canceled)
                {
                    return;
                }
                positions[i] = touches[i].position;
            }

            Vector2 center = CalcCenterOfPoints(positions);
            bool noneBegan = true;
            foreach (Touch touch in touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    initialDistance = CalcTotalDistFromPoint(positions, center);
                    initialScale = transform.localScale;
                    noneBegan = false;
                }
            }

            if (noneBegan)
            {
                float currentDistance = CalcTotalDistFromPoint(positions, center);
                if (Mathf.Approximately(initialDistance, 0)) return;

                float factor = currentDistance / initialDistance;


                SetScale(initialScale * factor);
            }

        }
    }

    AnimationCurve[] defaultLineScales = new AnimationCurve[0];
    void SetScale(Vector3 vector)
    {
        if (defaultLineScales.Length == 0)
        {
            defaultLineScales = new AnimationCurve[lines.Length];
        
            for (int i = 0; i < lines.Length; i++)
            {
                defaultLineScales[i] = lines[i].widthCurve;
            }
        }

        transform.localScale = vector;
        for (int i = 0; i <lines.Length; i++)
        {        
            Keyframe[] temp = new Keyframe[defaultLineScales[i].length];
        
            for (int j = 0; j < lines[i].widthCurve.length; j++)
            {
                temp[j].value = defaultLineScales[i].keys[j].value * vector.x;
            }
            lines[i].widthCurve = new AnimationCurve(temp);
            lines[i].widthCurve.preWrapMode = defaultLineScales[i].preWrapMode;
            lines[i].widthCurve.postWrapMode = defaultLineScales[i].postWrapMode;
        }
    }

    Vector2 CalcCenterOfPoints(Vector2[] points)
    {
        Vector2 total = Vector2.zero;
        for (int i = 0; i < points.Length; i++)
        {
            total += points[i];
        }

        return total / points.Length;
    }

    float CalcTotalDistFromPoint(Vector2[] points, Vector2 point)
    {
        float total = 0;
        foreach (Vector2 pt in points)
        {
            total += Vector2.Distance(point, pt);
        }
        return total;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        holder.SetActive(true);
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        SetScale(Vector3.one);
        holder.transform.localPosition = -centerPoint;
        holder.transform.rotation = Quaternion.identity;

        switch (scene.name)
        {
            case "VisualViewer":
            {
                transform.position = new Vector3(0, 0f, centerPoint.z * 4);
                freetransform = true;
                break;
            }

            case "AR":
            {
                SetScale(arSize * Vector3.one);
                holder.SetActive(false);
                freetransform = false;
                break;
            }

            case "CardboardVR":
            {
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 2));
                freetransform = false;
                break;
            }
        }
    }

    public void PlaceInAR(Pose hitPose)
    {
        holder.SetActive(true);
        holder.transform.position = hitPose.position;
        holder.transform.rotation = hitPose.rotation;
    }

}
