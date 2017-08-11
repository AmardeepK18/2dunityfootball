/*
https://docs.unity3d.com/ScriptReference/TextMesh.html - reference to text mesh - used in latest
version of unity version 2017.1
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This simple class is to display the score
public class Score : MonoBehaviour {
    public int score = 0;
	// Update is called once per frame
	void Update () {
        TextMesh txtMesh = this.GetComponent<TextMesh>();
        txtMesh.text = score.ToString();
    }
}
