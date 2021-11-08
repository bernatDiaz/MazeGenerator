using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementClick : MonoBehaviour
{
    private float forceMagnitude = 5000f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Vector3 direction = new Vector3(hit.point.x, hit.point.y, hit.point.z) - gameObject.transform.position;
                    direction.y = 0.0f;
                    Vector3 forceVector = direction.normalized * forceMagnitude * Time.deltaTime;
                    gameObject.GetComponent<Rigidbody>().AddForce(forceVector);
                }
            }
        }
    }
}
