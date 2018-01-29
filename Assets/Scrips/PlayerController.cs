using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Paddle m_Paddle;
    float spikeTimer = 0;
    float spikeTime = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float vel = Input.GetAxis("Vertical");
        m_Paddle.Move(vel);
        Spike();
	}

    void Spike()
    {
        //Debug.Log(m_Paddle.m_ReflectMode);
        spikeTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spikeTimer = spikeTime;
        }
        if (spikeTimer > 0.0f)
        {
            m_Paddle.m_ReflectMode = Paddle.ReflectMode.Spike;
            //Debug.Log(spikeTimer);
            
        }
        else
        {
            m_Paddle.m_ReflectMode = Paddle.ReflectMode.Reflect;
        }
    }
}
