using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GO : MonoBehaviour
{
    public int number = 10000;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = transform.position;

        
        Debug.Log("Start = " + Time.time);

        for (int i = 0; i < number; i++)
        {
            Physics.Raycast(position, Vector3.down);
        }

        Debug.Log("Total time = "+Time.time);

    }
}
