/*
script use to create the goal in the game
https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html - infomation 
on triggerenter2D.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//The goal class is to detect the collision with the goal and score a point 
public class Goal : MonoBehaviour {
    //to know which goal it is (left team's goal or right team's)
    public bool isLeft = true;
    //this method is called every time we detect a collision with the goal
    void OnTriggerEnter2D(Collider2D other)
    {
        //being sure if the collider is the ball
        if (other.gameObject.tag.Equals("Ball"))
        {
            //to know which team's goal it is 
            if (isLeft)
            {
                //score a goal to the adversary team
                Score score = GameObject.FindGameObjectWithTag("RightScore").GetComponent<Score>();
                score.score += 1;
                //is to get the ball back to the center
                initiate();
            }
            else
            {
                Score score = GameObject.FindGameObjectWithTag("LeftScore").GetComponent<Score>();
                score.score += 1;
                initiate();
            }
        }
            

    }
    //to initiate the position of the ball to the center
    void initiate()
    {
        
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        ball.transform.localPosition = new Vector2(0,0);
    }
}
