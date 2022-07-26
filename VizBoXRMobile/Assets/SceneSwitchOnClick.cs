using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneSwitchOnClick : MonoBehaviour
{
	public Button btn;
	public string sceneName;
	void Start()
	{
		//Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
		btn.onClick.AddListener(ChangeScenePlease);
	}
	public void ChangeScenePlease()
	{
		SceneManager.LoadScene(sceneName);
	}
}
