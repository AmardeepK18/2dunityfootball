/*
links for reference - 
https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html - rb.velocity
http://answers.unity3d.com/questions/792862/vector1-vector2-vector3.html - reinforce
understanding of different vectors and their purposes. 
https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
*/
/*
https://forum.unity3d.com/threads/initializing-a-vector3-in-c.161046/ - I have directly used
the line of code from this link to initialise vector (public Vector3 cameraOffset = new Vector3(0.0f,35.0f,-20.0f);) -
direct from this code
http://answers.unity3d.com/questions/706134/gameobjectfindgameobjectwithtag.html - 
I directly followed this link on gameobjectwithtag and also transform in relation to variables
I have also used the steering slides on learn.gold VLE
*/
/*
Here are some tutorials that I have followed in order to help with the scripts:
https://unity3d.com/learn/tutorials/projects/roll-ball-tutorial/moving-player?playlist=17141
^ for the rigidbody and the float value speed of the player more specifically (public float speed;
private Rigidbody rb; and rb = GetComponent<Rigidbody>(); are used indetically in my wwork taken from the link referenced above.)
https://unity3d.com/learn/tutorials/projects/adventure-game-tutorial/player?playlist=44381 -
player movement
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//the player class where all states and behaviors of a player are gathered
public class Player : MonoBehaviour {

    public Rigidbody2D rb;
    public Vector3 initialPosition = new Vector3(0,0,0);
    public float playerSpeed = 2;
    public bool directionH = true;
    public bool hasBall = false;
    public bool withLeftTeam = true;
    public bool isKeeper = false;
    public bool isDefense = false;
    //initializing the position of the player
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        transform.localPosition = initialPosition;
        
    }
    // Update is called once per frame
    void FixedUpdate () {
        //choose a random speed for the player
        this.playerSpeed = Random.Range(0.25f, 2);
        Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        //if the game starts
        if (game.start)
        {
            //detects if need to change the direction of the player
            if (rb.velocity.x > 0 && !directionH && !isKeeper)
                changeDirectionH();
            else if (rb.velocity.x < 0 && directionH && !isKeeper)
                changeDirectionH();
            //adding the steering to the velocity to make the player move towards the target
            rb.velocity += steer();
        }
              	
	}
    //the steering method
    Vector2 steer()
    {
        //get all the possible targets of the player 
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        GameObject RightGoal = GameObject.FindGameObjectWithTag("RightGoal");
        GameObject LeftGoal = GameObject.FindGameObjectWithTag("LeftGoal");
        Vector2 target;
        //depending on the state of a player set his state 
        if (isKeeper && withLeftTeam) 
        {
            target.x = LeftGoal.transform.position.x+0.5f;
            target.y = ball.transform.position.y;

        }else if(isKeeper && !withLeftTeam)
        {
            target.x = RightGoal.transform.position.x-0.5f;
            target.y = ball.transform.position.y;
        }
        else
        {
            if (!this.hasBall)
            {
                target.x = ball.transform.position.x;
                target.y = ball.transform.position.y;
            }
            else if (this.withLeftTeam)
            {
                target.x = RightGoal.transform.position.x;
                target.y = RightGoal.transform.position.y;
                
            }
            else
            {
                target.x = LeftGoal.transform.position.x;
                target.y = LeftGoal.transform.position.y;
                
            }
        }
        
           
        //calculate the steering base on the player position and the target already set
        Vector2 PlayerPos;
        PlayerPos.x = transform.position.x;
        PlayerPos.y = transform.position.y;
        Vector2 sep = target - PlayerPos;
        Vector2 desired = sep.normalized*playerSpeed;
        return desired - rb.velocity;
        
    }
    //this method changes the drection of the player 
    void changeDirectionH()
    {
        directionH = !directionH;
        transform.localScale *= -1;
    }
    
  

}
