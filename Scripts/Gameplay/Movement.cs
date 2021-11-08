using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Movement
{
    public static float forceMagnitude = 5000f; 
    public static void RaycastMove(Vector3 position, GameObject gameObject, float deltaTime)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                Vector3 direction = new Vector3(hit.point.x, hit.point.y, hit.point.z) - gameObject.transform.position;
                direction.y = 0.0f;
                Vector3 forceVector = direction.normalized * forceMagnitude * deltaTime;
                gameObject.GetComponent<Rigidbody>().AddForce(forceVector);
            }
        }
    }
    public static void ScreenToWorldPointMove(Vector3 position, GameObject gameObject, float deltaTime)
    {
        position.z = Camera.main.transform.position.y;
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(position);
        touchPosition.y = gameObject.transform.position.y;
        Vector3 direction = touchPosition - gameObject.transform.position;
        Vector3 forceVector = direction.normalized * forceMagnitude * deltaTime;
        gameObject.GetComponent<Rigidbody>().AddForce(forceVector);
    }
}
