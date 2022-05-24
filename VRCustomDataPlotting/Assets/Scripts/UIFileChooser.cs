using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;

public class UIFileChooser : MonoBehaviour
{
	

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void GetPath()
	{
		FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".jpg", ".png"));
		FileBrowser.SetDefaultFilter(".jpg");
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
		FileBrowser.AddQuickLink("Users", "C:\\Users", null);


		FileBrowser.ShowLoadDialog((paths) => { path = paths[0]; },
								   () => { Debug.Log("Canceled"); },
								   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select");

		// Coroutine example
		StartCoroutine(ShowLoadDialogCoroutine());
	}
}
