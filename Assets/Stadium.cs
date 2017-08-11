/*
Links referenced
https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html - infomation 
on triggerenter2D.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this class is to represent the borders of the stadium
public class Stadium : MonoBehaviour {
    //detect the collision with the borders 
    void OnTriggerEnter2D(Collider2D other)
    {
        //nitialize the position of the ball
        if (other.gameObject.tag.Equals("Ball"))
        {
            other.transform.position = new Vector2(0, 0);
        }
    }
}
