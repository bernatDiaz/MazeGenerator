using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGyro : MonoBehaviour
{
    private bool gyroEnabled = false;
    private Gyroscope gyroscope;
    private float forceMagnitude = 5000.0f;
    // Start is called before the first frame update
    void Start()
    {
        gyroEnabled = EnableGyro();
        if (!gyroEnabled)
        {
            NoGyroMessage();
        }
    }
    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
            return true;
        }
        return false;
    }

    private void NoGyroMessage()
    {
        Debug.Log("No gyro");
    }
    // Update is called once per frame
    void Update()
    {
        if (gyroEnabled)
        {
            Vector3 forceVector = ForceVector(Time.deltaTime);
            gameObject.GetComponent<Rigidbody>().AddForce(forceVector);
        }
    }
    private Vector3 ForceVector(float deltaTime)
    {
        Quaternion phoneRotation = gyroscope.attitude;
        phoneRotation = GyroToUnity(phoneRotation);

        
        Vector3 x = phoneRotation * Vector3.right;
        Vector3 y = phoneRotation * Vector3.up;
        Vector3 z = phoneRotation * Vector3.forward;

        //DebugAngles(x, y, z);

        Vector3 forceVector = new Vector3(x.z, 0, y.z);
        forceVector = forceVector * forceMagnitude * deltaTime;
        return forceVector;
    }
    private void DebugAngles(Vector3 x, Vector3 y, Vector3 z)
    {
        Debug.Log("x: " + x + ", y: " + y + ", z: " + z);
    }
    private Vector3 ForceVectorInverse(float deltaTime)
    {
        Quaternion phoneRotation = gyroscope.attitude;
        phoneRotation = GyroToUnity(phoneRotation);
        Quaternion inverse = Quaternion.Inverse(phoneRotation);

        Vector3 phoneInverse = inverse * Vector3.left;
        phoneInverse = phoneInverse.normalized;
        Vector3 forceVector = new Vector3(phoneInverse.y, 0, -phoneInverse.x);
        forceVector = forceVector * forceMagnitude * deltaTime;
        return forceVector;
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

}

