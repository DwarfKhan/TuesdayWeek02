﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour {

    public Rigidbody m_Rigidbody;

    public float m_StartSpeed;
    float m_Speed;
    float m_DisplaySpeed;
    float m_Multi = 1f;
    float m_StuckTimer;
    float m_StuckTime = 5f;
    float m_SpeedCreep = 1.07f;
    float m_SpikeSpeedCreep = .2f;

    public Text txtSpeed;
    public GameObject trail;
    public GameObject m_Ball;
    bool m_Spiked = false;
    public bool m_IsSplitBall;

    Vector3 m_Orientation;
    Vector3 startPos;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        Respawn(true);        
	}
	
	// Update is called once per frame
	void Update () {
        Unstuck();
        Particle();
        m_DisplaySpeed = m_Speed * m_Multi;
        txtSpeed.text = m_DisplaySpeed.ToString();
	}

    void Split()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        m_StuckTimer = m_StuckTime;
        Debug.Log("doink");
        m_Rigidbody.velocity = Vector3.zero;

        var paddle = collision.gameObject.GetComponent<Paddle>();
        var missile = collision.gameObject.GetComponent<Missile>();
        if (paddle != null)
        {
           m_Orientation = paddle.Reflect(m_Orientation, transform.position, ref m_Spiked);
            if (m_Spiked)
            {
                m_Orientation.y = 0;
                m_Orientation = m_Orientation.normalized;
                m_Multi += m_SpikeSpeedCreep;
            }
            else
            {
                m_Multi = 1f;
            }

            m_Speed *= m_SpeedCreep;
            m_Rigidbody.AddForce(m_Orientation.normalized * m_Speed * m_Multi);

        }
        else if(missile != null){
            Split();
        }
        else
        {
            Respawn();
        }




        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        //if (collision.relativeVelocity.magnitude > 2)
        //    audioSource.Play();
    }

    void Respawn(bool startGame = false)
    {
        if (m_IsSplitBall)
        {
            m_Ball.SetActive(startGame);
        }
        transform.position = startPos;
        m_Speed = m_StartSpeed;
        m_Multi = 1f;
        Vector3 startOrientation = Vector3.zero;
        startOrientation.x = Random.Range(-1.0f, 1.0f);
        startOrientation.y = Random.Range(-.5f, .5f);
        m_Orientation = startOrientation;
        m_Rigidbody.AddForce(m_Orientation.normalized * m_Speed);

    }

    void Unstuck()
    {
        m_StuckTimer -= Time.deltaTime;
        if (m_StuckTimer > 0)
        {
            return;
        }
        Respawn();
        m_StuckTimer = m_StuckTime;

    }

    void Multi()
    {
        if (Mathf.Abs(m_Orientation.y) > 0)
        {
            m_Multi = 1;
        }
        else
        {
            m_Multi+= 0.1f;
        }
    }

    void Particle()
    {
        if (m_DisplaySpeed > 1600)
        {
            trail.SetActive(true);
        }
        else
        {
            trail.SetActive(false);
        }
    }
}
