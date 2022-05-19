using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum Genders
{
    male,
    female,
    other
}

public class UserData : MonoBehaviour
{
    public Genders gender;
    public int ageGroup, pClass;
    public Vector2 ages;

    // Start is called before the first frame update
    void Start()
    {
        //height = Camera.main.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeGender(TMP_Dropdown dropdown)
    {
        gender = (Genders)dropdown.value;
    }

    public void ChangeAge(TMP_Dropdown dropdown)
    {
        ageGroup = dropdown.value;
        switch(ageGroup)
        {
            case 0: ages = new Vector2(0, 100);
                break;
            case 1: ages = new Vector2(0, 17);
                break;
            case 2: ages = new Vector2(18, 23);
                break;
            case 3: ages = new Vector2(24, 29);
                break;
            case 4:
                ages = new Vector2(30, 37);
                break;
            case 5:
                ages = new Vector2(38, 49);
                break;
            case 6:
                ages = new Vector2(50, 100);
                break;
        }
    }

    public void ChangePClass(TMP_Dropdown dropdown)
    {
        pClass = dropdown.value;
    }
}
