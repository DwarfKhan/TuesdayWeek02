using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public Paddle m_Paddle;
    public Transform m_FollowTarget;
    public Vector3 m_Offset = Vector3.zero;
    public float m_RefTypeTimer;
    public float m_RefTypeTime = 3;
    public float m_OffsetTimer;
    public float m_OffsetTime = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChooseType();
        Offset();
        float diff = (m_FollowTarget.position.y + m_Offset.y) - transform.position.y;
        if (diff > 0)
        {
            m_Paddle.Move(1.0f);
        }
        else
        {
            m_Paddle.Move(-1.0f);
        }
    }

    void ChooseType()
    {
        m_RefTypeTimer -= Time.deltaTime;
        if (m_RefTypeTimer > 0)
        {
            return;
        }
        if ((int)Random.Range(0,4) == 0)
        {
            m_Paddle.m_ReflectMode = Paddle.ReflectMode.Spike;
        }
        else
        {
            m_Paddle.m_ReflectMode = Paddle.ReflectMode.Reflect;

        }
        m_RefTypeTimer = m_RefTypeTime;
    }

    void Offset()
    {
        m_OffsetTimer -= Time.deltaTime;
        if (m_OffsetTimer < 0)
        {
        m_Offset.x = Random.Range(-5.0f, 5.0f);
        m_Offset.y = Random.Range(-5.0f, 5.0f);
            Debug.Log(m_Offset);
            m_OffsetTimer = m_OffsetTime;
        }

    }

}
