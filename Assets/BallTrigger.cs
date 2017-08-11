/*
Links referenced
https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html - infomation 
on triggerenter2D.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this class is used to trigger the collision with the ball
public class BallTrigger : MonoBehaviour {
    //every time a collision is detected we call this method
    void OnTriggerEnter2D(Collider2D other)
    {
        //detects if the ball is colliding with a player
        if (other.gameObject.tag.Equals("Player"))
        {
            //if true we make the player has the ball 
            other.GetComponent<Player>().hasBall = true;
        }
    }
    //when the collision is no more detected we call this method
    void OnTriggerExit2D(Collider2D other)
    {
        //detects if the collider is a player
        if (other.gameObject.tag.Equals("Player"))
        {
            //make the player dosn't have the ball
            other.GetComponent<Player>().hasBall = false;
        }
    }
}
