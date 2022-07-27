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
    bool rotationInputLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        holder = transform.GetChild(0).gameObject;
        centerPoint = holder.GetComponent<Holder>().CalculateCenterPoint();
        transform.position = new Vector3(0, 0f, centerPoint.z*4);
    }

    private void Update()
    {
        if (!rotationInputLocked)
        {
            DoRotation();
        }
    }

    private void DoRotation()
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        holder.SetActive(true);
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        holder.transform.localPosition = -centerPoint;
        holder.transform.rotation = Quaternion.identity;

        switch (scene.name)
        {
            case "VisualViewer":
            {
                transform.position = new Vector3(0, 0f, centerPoint.z * 4);
                rotationInputLocked = false;
                break;
            }

            case "AR":
            {
                transform.localScale = 0.01f * Vector3.one;
                holder.SetActive(false);
                rotationInputLocked = true;
                break;
            }

            case "CardboardVR":
            {
                transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 2));
                    rotationInputLocked = true;
                    break;
            }
        }
    }

    public void PlaceInAR(Pose hitPose)
    {
        holder.gameObject.SetActive(true);
        holder.transform.position = hitPose.position;
        holder.transform.rotation = hitPose.rotation;
    }
}
