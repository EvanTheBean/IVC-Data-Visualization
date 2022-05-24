using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;

public class UIFileChooser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
	{ 
		GetPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void GetPath()
	{
		FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".csv"));
		FileBrowser.SetDefaultFilter(".csv");
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
		FileBrowser.AddQuickLink("Users", "C:\\Users", null);

		StartCoroutine(ShowLoadDialogCoroutine());
	}

	IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select .csv file", "Load");

		// Dialog is closed
		// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
		Debug.Log(FileBrowser.Success);

		if (FileBrowser.Success)
		{
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			for (int i = 0; i < FileBrowser.Result.Length; i++)
				Debug.Log(FileBrowser.Result[i]);

			DataReader.Instance.Read(FileBrowser.Result[0]);
		}
	}
}
