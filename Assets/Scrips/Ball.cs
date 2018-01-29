using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour {

    public Rigidbody m_Rigidbody;
     float m_Speed;
    float m_DisplaySpeed;
    float m_multi = 1f;
    float m_StuckTimer;
    float m_StuckTime = 5f;
    float m_speedCreep = 1.07f;
    float m_SpikeSpeedCreep = .2f;
    public float m_StartSpeed;
    public Text txtSpeed;
    public GameObject trail;
    bool m_Spiked = false;

    Vector3 m_Orientation;
    Vector3 startPos;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        Respawn();        
	}
	
	// Update is called once per frame
	void Update () {
        Unstuck();
        Particle();
        m_DisplaySpeed = m_Speed * m_multi;
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
                m_multi += m_SpikeSpeedCreep;
            }
            else
            {
                m_multi = 1f;
            }

            m_Speed *= m_speedCreep;
            m_Rigidbody.AddForce(m_Orientation.normalized * m_Speed * m_multi);

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

    void Respawn()
    {
        transform.position = startPos;
        m_Speed = m_StartSpeed;
        m_multi = 1f;
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
            m_multi = 1;
        }
        else
        {
            m_multi+= 0.1f;
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
