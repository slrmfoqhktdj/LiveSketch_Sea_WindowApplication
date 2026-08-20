using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creation_Stringray : MonoBehaviour
{
  
    public float speed = 10f;

    // public float turnspeed = 1f;

    [Header("Unity Stuff")]
    private Transform target;
    private Transform startpoint;
    private int wavepointIndex = 0;




    private Vector3 dir;

    private Transform[] points;

    public void MyWayPoints(int num)
    {
        startpoint = StringrayStartPoints.startpoints[num];
        GetWaypoints(startpoint);
        target = points[0];
        Debug.Log(startpoint);

    }


    // Update is called once per frame
    void Update()
    {
        
        dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime , Space.World);
        

        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {           
            transform.Rotate(new Vector3(0f, 0f, 180f), Space.Self);
                
            GetNextWaypoint();
        }


    }


     void GetNextWaypoint()
    {
        if(wavepointIndex >= points.Length -1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = points[wavepointIndex];

        // transform.Rotate(Vector3.up * Time.deltaTime * 2);
    }


    void EndPath()
    {
        Destroy(gameObject);
        //  Destroy(gameObject,5f); 5초후 사라지게
    }

    void GetWaypoints(Transform startpoint)
    {
        
        points = new Transform[startpoint.childCount];
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = startpoint.GetChild(i);
        }
        
    }
}
