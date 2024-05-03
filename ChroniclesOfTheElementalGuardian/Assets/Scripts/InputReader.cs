using System;
using UnityEngine;

public class InputReader : Singleton<InputReader>
{
    public static event Action InputD;
    public static event Action InputA;
    public static event Action InputW;
    public static event Action InputS;
    public static event Action InputSpace;
    public static event Action InputE;
    public static event Action InputR;
    public static event Action InputTab;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            InputD?.Invoke();
        }   
        if(Input.GetKey(KeyCode.A))
        {
            InputA?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            InputW?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            InputS?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InputSpace?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            InputE?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            InputR?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            InputTab?.Invoke();
        }
    }
}
