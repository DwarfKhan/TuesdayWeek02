using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Missile : MonoBehaviour {

    [SerializeField]Transform m_Target;
    [SerializeField]float m_Speed;
    Rigidbody m_rb;

	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = m_Target.position - transform.position;
        Vector3 original = transform.position;
        diff.z = 0;
        original.z = 0;
        float diffAngle = Vector3.Angle(diff, transform.forward);
        //Debug.Log(diffAngle);

        Vector3 localDif = transform.InverseTransformPoint(diff);

        if (localDif.x < 0)
        {
            //diffAngle *= -1;
            //diffAngle = diffAngle * -1;
            diffAngle = -diffAngle;
        }

        m_rb.AddForce(transform.forward * m_Speed);
        m_rb.AddTorque(transform.up * diffAngle * 0.04f);
	}
}
