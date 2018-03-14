using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {


    GameObject level01;
    GameObject level02;
    GameObject level03;
    GameObject level04;
    GameObject level05;


    

	void Start ()
    {
        level01 = GameObject.Find("Level01");
        level02 = GameObject.Find("Level02");
        level03 = GameObject.Find("Level03");
        level04 = GameObject.Find("Level04");
        level05 = GameObject.Find("Level05");




    }
	
	void Update ()
    {
	
	}

    public void ButtonHover (Button button)
    {
        if (button.name == "Level01")
        {
            level01.transform.SetAsLastSibling();
    
        }
        else if (button.name == "Level02")
        {
            level02.transform.SetAsLastSibling();
        }
        else if (button.name == "Level03")
        {
            level03.transform.SetAsLastSibling();
        }

        else if (button.name == "Level04")
        {
            level04.transform.SetAsLastSibling();
        }

        else if (button.name == "Level05")
        {
            level05.transform.SetAsLastSibling();
        }
    }
    public void ButtonClick (Button button)
    {
        if (button.name == "Level01")
        {
            Application.LoadLevel("Level01");
            Debug.Log("Load Level01");
        }
        else if (button.name == "Level02")
        {
            Application.LoadLevel("Level02");
            Debug.Log("Load Level02");
        }
        else if (button.name == "Level03")
        {
            Application.LoadLevel("Level03");
            Debug.Log("Load Level03");
        }
    

        else if (button.name == "Level04")
        {
            Application.LoadLevel("Level04");
            Debug.Log("Load Level04");
        }

        else if (button.name == "Level05")
        {
            Application.LoadLevel("Level05");
            Debug.Log("Load Level05");
        }
    }

}
