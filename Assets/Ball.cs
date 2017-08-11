/*
Here are the links used in this script to help me
http://answers.unity3d.com/questions/792862/vector1-vector2-vector3.html - reinforce
understanding of different vectors and their purposes. 
https://docs.unity3d.com/ScriptReference/Transform-localPosition.html -
research into transform
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class represents the behavior the ball is going to have
public class Ball : MonoBehaviour {
    // this attribute is to have the center of the stadium as an initial postion of the ball 
    public Vector2 intialPosition = new Vector2(0,0);
    
    void Start()
    {
        //initiate the position of the ball to be at the initial position
        transform.localPosition = this.intialPosition;
    }
    

}
