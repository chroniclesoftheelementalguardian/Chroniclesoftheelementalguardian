using System;
using UnityEngine;

public class InputReader : Singleton<InputReader>
{
    public static event Action MoveRightPressed;
    public static event Action MoveRightUnpressed;
    public static event Action MoveLeftPressed;
    public static event Action MoveLeftUnpressed;
    public static event Action InputW;
    public static event Action InputS;
    public static event Action AttackPressed;
    public static event Action SkillPressed;
    public static event Action DefendPressed;
    public static event Action NextSkillPressed;
    public static event Action PreviousSkillPressed;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            MoveRightPressed?.Invoke();
        }   
        if(Input.GetKey(KeyCode.A))
        {
            MoveLeftPressed?.Invoke();
        }

        if(!Input.GetKey(KeyCode.D))
        {
            MoveRightUnpressed?.Invoke();
        }   
        
        if(!Input.GetKey(KeyCode.A))
        {
            MoveLeftUnpressed?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            InputW?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            InputS?.Invoke();
        }

        if(Input.GetMouseButtonDown(0))
        {
            AttackPressed?.Invoke();
        }

        if(Input.GetMouseButtonDown(1))
        {
            DefendPressed?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SkillPressed?.Invoke();
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            NextSkillPressed?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            PreviousSkillPressed?.Invoke();
        }
    }
}
