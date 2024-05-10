using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Credits : MonoBehaviour
{
    public static event Action CreditsFinished;
    
    //Animation event
    void onFinished()
    {
        CreditsFinished?.Invoke();
    }
}
