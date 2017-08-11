/*
Here are the links used for the c# script
https://docs.unity3d.com/ScriptReference/TextMesh.html - reference to text mesh - used in latest
version of unity version 2017.1
https://docs.unity3d.com/ScriptReference/Time-deltaTime.html - striaghforward reference to time 
in unity - used for the countDOWN cs script.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//this class is to represent the CountDown 
public class CountDown : MonoBehaviour {
    // a field to indicate the number of seconds the players are going to play
    public float count = 90;

	
	// Update is called once per frame
	void Update () {
        //get the game to know if it started or not yet
        Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        //if the game is started we start our count down
        if (game.start)
        {
            //if the count down ends load the first scene to try again
            if (count < 0)
            {
                SceneManager.LoadScene("project");
            }
            //every deltaTime we update the counter and modify the textMesh that represent it on the screen
            count -= Time.deltaTime;
            TextMesh txtMesh = this.GetComponent<TextMesh>();
            txtMesh.text = count.ToString("#.");
        }
        
    }
}
