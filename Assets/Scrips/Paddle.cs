using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float moveSpeed;
    public float range;
    public float verticalOrientationMultiplier = .7f; //used to keep the ball moving back and forth, with less steep angles
    public Vector3 startPos;
    public Vector3 normal;
    public bool isController;
    public enum ReflectMode {Reflect, Spike};
    public ReflectMode m_ReflectMode = ReflectMode.Reflect;
    [SerializeField]GameObject m_indicator;

    public void Start()
    {
        startPos = transform.position;
    }

    public void Move(float vel)
    {
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y + (vel * moveSpeed * Time.deltaTime), startPos.y - range, startPos.y + range );
        transform.position = pos;


        //transform.Translate(Vector3.up * vel * moveSpeed * Time.deltaTime);

   


    }

    public Vector3 Reflect(Vector3 orientation, Vector3 ballLocation,ref bool isSpiked)
    {
        if (m_ReflectMode == ReflectMode.Spike)
        {
            isSpiked = true;
        }
        else
        {
            isSpiked = false;
        }

        normal.Normalize();
        float distance = -1 * Vector3.Dot(normal, orientation);

        orientation += (normal * (distance * 2));


            if (isController)
            {
            orientation = DirectBall(orientation.normalized, ballLocation);
            }

        orientation.y *= verticalOrientationMultiplier;
        return orientation.normalized;

    }

    public Vector3 DirectBall(Vector3 orientation, Vector3 ballLocation)
    {
        float yDif = ballLocation.y - transform.position.y;
        Vector3 ans = orientation;
        ans.y += (yDif/3.0f);
        ans.y += Random.Range(-0.1f, 0.1f);

        return ans.normalized;
    }

    void Indicate()
    {
        if (m_indicator == null)
        {
            return;
        }
        if (m_ReflectMode != ReflectMode.Spike)
        {
            m_indicator.SetActive(false);
            return;
        }
        m_indicator.SetActive(true);
        Vector3 offset = Vector3.zero;
        offset.z = -2;
        m_indicator.transform.position = (transform.position + offset );
    }

    private void Update()
    {
        Indicate();
    }

}
