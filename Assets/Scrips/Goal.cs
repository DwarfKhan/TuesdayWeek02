using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Goal : MonoBehaviour {

    public Text score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        int num;
        int.TryParse(score.text,out num);
        num++;
        score.text = num.ToString();
    }
}
