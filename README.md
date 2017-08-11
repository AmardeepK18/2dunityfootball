# README - Amardeep Kalsi
The program is a unity project that simulates a football game using C# language.
This project was built in multiple steps. First was to add the different sprites
to the scene. Sprites are the different objects that will be manipulated via C# 
scripts. In order to run the project you download this project as a zip, and open
it within unity. You first press play in unity’s built in 
button and then press the green play button on the in game display.

The sprites used on this project are: 
•	Stadium: A background image of the stadium that the player will be playing 
    on.
•	Player1: An image to represent a player with the first team
•	Player2: An image to represent a player with the second team, the sprite 
    will be different to distinguish between the two separate football teams. 
•	Keeper1: An image to represent the goal keeper of the first team
•	Keeper2: An image to represent the goal keeper of the second team, the 
    sprite will also be different to the keeper1 sprite to distinguish the 
    separate teams. 
•	football: An image to represent the ball, the object that the players will 
    need to move around into the goal to score.
•	playButton: An image to represent the play button to launch the game

Objects hierarchy
•	Main Camera: Object provided by unity that manipulates the camera, this 
    object is not used because my game has one scene.
•	Football: this object represents the ball and contains a lot of components 
    that detect collision such as Collider Box 2D and RigidBody2D, these 
    components are a unity built-in components and another component is added to
    manipulate the position of the ball, this component is a csharp script 
    called Ball.cs.
o	ballTrigger: is a helper emptyObject that has a collider box with Trigger, 
    it is triggered when the player touches the ball. After this action, some 
    variables are changed to indicated that the player has the ball. 
•	Countdown: this object represents a timer in the top of the screen.
•	Stadium: it is the object that represents the stadium with colliders and the 
    borders.
•	playButton: the object to represent the button to click on to start the 
    game.
•	goalLeft: an empty object with a collider to detect if the ball is entered 
    to the left goal or not.
•	goalRight: an empty object with a collider to detect if the ball is entered 
    to the right goal or not.
•	scoreLeft: an empty object with the textMesh component to represent the 
    score of the team in the left.
•	scoreRight: an empty object with the textMesh component to represent the 
    score of the team in the right.
•	Team1: an empty object that regroups the first team and contains: 
o	Player1: the first player of the first team with a csharp script called 
    Player.cs and colliders and RigidBody2D to detect collision.
o	Player2: the second player of the first team.
o	Keeper: the goal keeper of the team with also a Player component.
•	Team2: like the team1 but this time to represent the team on the right.

C# Scripts
All of these objects use scripts that I wrote to indicate their behavior, a 
listing of this scripts is as follow: 
•	Ball.cs : a script to represent the ball, it has one attribute that is the 
    initialPosition of the ball that we need every time we want to reinitiate 
    its position.
•	BallTrigger.cs : the script responsible of detecting if the ball is touched 
    by a player to change the state of the player.
•	CountDown.cs : this script is keeping the update of the timer on the top of 
    screen based on the time that passed playing the game.
•	Game.cs: it’s a script that tell us if the game is started or not yet and it
    is attached to the stadium and contains one attribute (Boolean start) to 
    indicate if the game is started or not.
•	Goal.cs: this script is like BallTrigger but this time it is a trigger of 
    the goal. It detects if the ball touches the goal to increase score.
•	PlayButton.cs: it’s a simple code to hide the button when we press it to 
    begin the game.
•	Player.cs: it’s the main script of Player movement with a lot of variables 
    to indicate the state of the player: 
o	isKeeper (Boolean to indicate if the player is a goal keeper)
o	hasBall(Boolean to indicate if the player has the ball)
o	isDefense (to indicate if the player is in defense or not)
o	playerSpeed (a float that is generated randomly every time to give the game 
    some dynamics)
o	withLeftTeam (to know which team this player is with)
o	directionH(Boolean to know if the player is heading left or right)
•	Score.cs: it’s to update the text that contains the score of the teams.
•	Stadium.cs: a script to manipulate the different components linked to the 
    stadium. And the different methods that we call each time the ball touches 
    the borders.

Game Rules and Steering strategies: 
The rules of this game are so simple, we have two teams and each has three 
players one to attack and want to defend and another as a goal keeper. The 
objective is to carry the ball to the goal of the opposing team and if it 
touches the goal the team scores one. The game begins with a timer set to a 
certain number of seconds (90) that you can configure and the players run after 
the ball and try to make it touch the goal of the oppoenets team if so the ball
get back to the center and the game continues. If the ball touches the borders 
of the stadium it goes back to the center of the field. The players play the 
game using different strategies depending on the state of each player. The 
steering strategy is assured using this code in the player.cs script.
```
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
```
 It generates a random speed for the player to make it dynamic and then make sure 
if the player must change direction then changes his velocity by calling the 
method steer() that reacts differently depending on the player :

Goal Keeper:  
To make a player a goal keeper we should check the box iskeeper within unity.
The goalkeeper has a simple strategy, it is to follow the ball vertically and 
assure that it doesn’t pass to the goal. The goalkeeper will move up and down
in line with the verticality of the ball and try to prevent the ball from
passing the keeper and allowing the opposition to score a goal.

The code that makes the keeper move vertically to follow the ball position on 
the vertical line without leaving his goal is in the player.cs script and is as
follows:
```
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
```
Player without the ball:To make a player without the ball you should uncheck 
the has ball box option in the player script in unity. 

If a player doesn’t have the ball, he must be following it. This code is later 
on in the the player.cs script, and is as follows:
```
else
        {
            if (!this.hasBall)
            {
                target.x = ball.transform.position.x;
                target.y = ball.transform.position.y;
            }
```
This section of code is 
responsible for this action. In this section of code, we make the target of the
player a Vector that represents the ball’s position. 

Player with ball:If a player collides with the ball he adopts this strategy. 
This strategy makes the player looking for the goal of the oppositions team to 
score. The source code in the player.cs script which is responsible 
for this action within the game is as follows:
```
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
```
After setting the target, the steer function continues the calculations to 
return the correct value that will be added to the velocity and make the object 
moves towards the target. The code that is responsible for this part of the 
game is as follows:
```
//calculate the steering base on the player position and the target already set
        Vector2 PlayerPos;
        PlayerPos.x = transform.position.x;
        PlayerPos.y = transform.position.y;
        Vector2 sep = target - PlayerPos;
        Vector2 desired = sep.normalized*playerSpeed;
        return desired - rb.velocity;
```   

Video Link - https://www.dropbox.com/s/abx7lthtro3xu6g/gameaivideo.mov?dl=0

This video showcases my game when played. The duration is one game - 90 seconds.
I have built and run the game again, hence why the game is shown how it is.


References for source code – also found as comments in source code files. 

Ball.cs
Here are the links used in this script to help me
http://answers.unity3d.com/questions/792862/vector1-vector2-vector3.html - 
reinforce understanding of different vectors and their purposes. 
https://docs.unity3d.com/ScriptReference/Transform-localPosition.html -
research into transform

Balltrigger.cs
Links referenced
https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html - 
infomation on triggerenter2D.

Countdown.cs
Here are the links used for the c# script
https://docs.unity3d.com/ScriptReference/TextMesh.html - reference to text mesh
- used in latest version of unity version 2017.1
https://docs.unity3d.com/ScriptReference/Time-deltaTime.html - striaghforward 
reference to time in unity - used for the countDOWN cs script.

Goal.cs
script use to create the goal in the game
https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html 
- infomation on triggerenter2D.

Playbutton.cs
https://docs.unity3d.com/ScriptReference/Input.GetMouseButtonDown.html - 
used for the mouse to press the play button

Player.cs
links for reference - 
https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html - rb.velocity
http://answers.unity3d.com/questions/792862/vector1-vector2-vector3.html - 
reinforce understanding of different vectors and their purposes. 
https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
https://forum.unity3d.com/threads/initializing-a-vector3-in-c.161046/ - 
I have reinforced knowledge of initialising vector
vector (public Vector3 cameraOffset = new Vector3(0.0f,35.0f,-20.0f);) - 
direct from this code 
http://answers.unity3d.com/questions/706134/gameobjectfindgameobjectwithtag.html
- I directly followed this link on gameobjectwithtag and also transform in 
relation to variables
I have also used the steering slides on learn.gold VLE – as a reference
Here are some tutorials that I have followed in order to help with the scripts:
https://unity3d.com/learn/tutorials/projects/
roll-ball-tutorial/moving-player?playlist=17141
^ for the rigidbody and the float value speed of the player more specifically 
(public float speed; private Rigidbody rb; and rb = GetComponent<Rigidbody>(); 
are used in my work taken from the link referenced above.)
https://unity3d.com/learn/tutorials/projects/adventure-game-tutorial/
player?playlist=44381 - player movement

score.cs
https://docs.unity3d.com/ScriptReference/TextMesh.html - reference to text mesh 
- used in latest version of unity version 2017.1

Stadium.cs
Links referenced
https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html 
- infomation on triggerenter2D.




