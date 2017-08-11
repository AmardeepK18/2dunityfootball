using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this class is only to know if the game is started or not yet
public class Game : MonoBehaviour {
    //this field is the one representing the state of the game 
    public bool start;
	// initialise the game as it is not started
	void Start () {
        start = false;
	}
	

}
