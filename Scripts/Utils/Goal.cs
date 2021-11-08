using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void GoalReached();
    public event GoalReached OnGoal;

    private void OnTriggerEnter(Collider other)
    {
        if(OnGoal != null)
            OnGoal();
    }
}
