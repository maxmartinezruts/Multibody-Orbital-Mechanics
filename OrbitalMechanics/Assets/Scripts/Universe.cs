using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Universe : MonoBehaviour
{
    public float G = 1f;
    public float deltaT;
    public int iterations;
    public Body[] bodies;

    public Slider sliderG;
    public Text textG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        G = sliderG.value;
        textG.text = G.ToString();

        
        for (int i = 0; i < iterations; i++)
        {
            foreach (var body in bodies)
            {
                 body.UpdateVirtualVel(bodies, deltaT);   
            }
            
            foreach (var body in bodies)
            {
                body.UpdateVirtualPos(deltaT);  
            }
        }
        foreach (var body in bodies)
        {
            body.updateTrajectory();
            body.ResetVirtual();
        }
    }
}
