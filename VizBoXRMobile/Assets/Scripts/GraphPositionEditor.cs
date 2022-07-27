using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GraphPositionEditor : MonoBehaviour
{
    GameObject holder;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        holder = transform.GetChild(0).gameObject;
        transform.position = new Vector3(0, -1.24f, 4.26f);

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        holder.SetActive(true);
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        holder.transform.localPosition = -holder.GetComponent<Holder>().CalculateCenterPoint();
        holder.transform.rotation = Quaternion.identity;

        if (scene.name == "VisualViewer")
        {
            transform.position = new Vector3(0, -1.24f, 4.26f);
        }

        if (scene.name == "AR")
        {
            transform.localScale = 0.01f * Vector3.one;
            holder.SetActive(false);
        }

        if (scene.name == "CardboardVR")
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 2));
        }
    }

    public void PlaceInAR(Pose hitPose)
    {
        holder.gameObject.SetActive(true);
        holder.transform.position = hitPose.position;
        holder.transform.rotation = hitPose.rotation;
    }
}
