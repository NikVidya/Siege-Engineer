using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {


    GameObject level01;
    GameObject level02;
    GameObject level03;
    GameObject level04;
    GameObject level05;

	public string level;
    

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
      
    }
    public void ButtonClick (Button button)
    {

		Application.LoadLevel (level);

	 }

}
