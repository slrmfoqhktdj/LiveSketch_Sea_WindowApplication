using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creation_Shark : MonoBehaviour
{

    public float speed = 10f;

    public float turnspeed = 1f;

    [Header("Unity Stuff")]
    private Transform target;
    private Transform startpoint;
    private int wavepointIndex = 0;
    private bool runstart = true;



    private Vector3 dir;

    private Transform[] points;

    // void Start(){
    //     // transform.rotation = Quaternion.Euler(0,180,0);
    //      Debug.Log("Shark Created");
    // }

    public void MyWayPoints(int num)
    {
        startpoint = SharkStartPoints.startpoints[num];
        GetWaypoints(startpoint);
        target = points[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(runstart){
            dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime , Space.World);
        }

        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {           

            // if(rootnumber != 1){
            //     if(transform.rotation.eulerAngles.y <= 180f){
             
            //         transform.Rotate(new Vector3(10f, 180f, -10f)* turnspeed * Time.deltaTime, Space.World);
            //         runstart = false;
            //     }else{
            //         runstart = true;
            //         GetNextWaypoint();
            //     }
            // }else{
            //     if(transform.rotation.eulerAngles.y > 2f){
             
            //         transform.Rotate(new Vector3(0f, -180f, 50f)* turnspeed * Time.deltaTime, Space.World);
            //         runstart = false;
            //     }else{
            //         runstart = true;
            //         GetNextWaypoint();
            //     }
            // }
                
            transform.Rotate(new Vector3(0f, 180f, 0f), Space.Self);
                
            GetNextWaypoint();




            
        }


        // 움직임
        // dir = target.position - transform.position;
        // transform.Translate(dir.normalized * speed * Time.deltaTime , Space.World);
  

        // 방향성
        // Quaternion lookRotation = Quaternion.LookRotation(dir);
        // Vector3 rotation = Quaternion.Lerp(transform.rotation,lookRotation,Time.deltaTime*turnspeed).eulerAngles;
        // transform.rotation = Quaternion.Euler (rotation.x,0f,0f);

        // Quaternion targetRot = Quaternion.LookRotation(target.transform.position);
        // transform.rotation = Quaternion.Lerp(transform.rotation,targetRot,Time.deltaTime*turnspeed);



        // if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        // {            
        //         GetNextWaypoint();
                
        //         Debug.Log("turn");
        // }


      
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
