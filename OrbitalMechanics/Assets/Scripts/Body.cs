using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{

    public float mass;
    
    public Vector3 vel_ini;

    public List<Vector3> trajectory;

    Vector3 virtualPos;
    Vector3 virtualVel;
    Vector3 vel;

    public Universe universe;

    // Start is called before the first frame update
    void Start()
    {
        vel = vel_ini;
        ResetVirtual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVirtualVel(Body[] bodies, float deltaTime){
        foreach (var otherBody in bodies)
        {
            if (otherBody != this){
                float dist = Vector3.Distance(virtualPos, otherBody.virtualPos);
                Vector3 forceDir = Vector3.Normalize(otherBody.virtualPos - virtualPos);
                Vector3 force = forceDir *  universe.G * mass * otherBody.mass / Mathf.Pow(dist,2);
                Vector3 acceleration = force/mass;
                virtualVel += acceleration * deltaTime;
            }
        }
    }

      public void UpdateVel(Body[] bodies, float deltaTime){
        foreach (var otherBody in bodies)
        {
            if (otherBody != this){
                float dist = Vector3.Distance(transform.position, otherBody.transform.position);
                Vector3 forceDir = Vector3.Normalize(otherBody.transform.position - transform.position);
                Vector3 force = forceDir *  universe.G * mass * otherBody.mass / Mathf.Pow(dist,2);
                Vector3 acceleration = force/mass;
                vel += acceleration * deltaTime;
            }
        }
    }

    public void UpdateVirtualPos(float deltaTime){
        virtualPos += virtualVel * deltaTime;
        trajectory.Add(virtualPos);
    }

    public void ResetVirtual(){
        virtualPos = transform.position;
        virtualVel = vel_ini;
        trajectory = new List<Vector3>();

    }
     public void UpdatePos( float deltaTime){
        transform.position += vel * deltaTime;
        
    }

    public void updateTrajectory(){
        GetComponent<LineRenderer>().positionCount = trajectory.Count;              // Number of points of the line that defines the path
        for (int i =0; i< trajectory.Count; i++){
            GetComponent<LineRenderer>().SetPosition(i, trajectory[i]);    // Create a new pivot in line renderer         
        }   
   
    }
}
