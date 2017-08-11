/*
https://docs.unity3d.com/ScriptReference/Input.GetMouseButtonDown.html - used for the mouse
to press the play button
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this class is to manipulate the play button
public class PlayButton : MonoBehaviour {


	
	// detect every frame if the play button was clicked to start the game
	void Update () {
        Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        
        if (game.start ==false && Input.GetMouseButtonDown(0))
        {
            hideBtn();
                game.start = true;

        }

    }
    //to show the button
    public void showBtn()
    {
        gameObject.SetActive(true);

    }
    //to hide the button
    public void hideBtn()
    {
        gameObject.SetActive(false);

    }
   
}
