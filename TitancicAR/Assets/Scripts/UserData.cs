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
    public float height;
    public Vector2 ages;

    // Start is called before the first frame update
    void Start()
    {
        //height = Camera.main.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        height = Camera.main.gameObject.transform.position.y * 1.1f;
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
            case 0: ages = new Vector2(0, 10);
                break;
            case 1: ages = new Vector2(11, 18);
                break;
            case 2: ages = new Vector2(18, 25);
                break;
            case 3:
                ages = new Vector2(26, 35);
                break;
            case 4:
                ages = new Vector2(36, 50);
                break;
            case 5:
                ages = new Vector2(51, 100);
                break;
        }
    }

    public void ChangePClass(TMP_Dropdown dropdown)
    {
        pClass = dropdown.value;
    }
}
