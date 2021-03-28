using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;
    
    public UnityEvent swipeEvent;
    //public UnityEvent touchEvent;

    private const float MIN_SWIPE = 100;
    private enum Directions
    {
        Left,
        Right,
        Down,
        Up
    };
    
    [HideInInspector] public bool[] swipes = new bool[4];

    private Vector2 startTouchPos; // start touch position
    private Vector2 swipeDelta; // swipe vector
    private bool isSwipeStarted; 

    Vector2 TouchPosition() { return Input.mousePosition; }

    private void Awake()
    {
        Application.targetFrameRate = 140;
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSwipeStarted = true;
            startTouchPos = TouchPosition();
        }

        if (Input.GetMouseButtonUp(0) && isSwipeStarted)
        {
            isSwipeStarted = false;
            SendSwipe();
        }
        
        if (Input.GetMouseButton(0) && isSwipeStarted)
        {
            swipeDelta = startTouchPos - TouchPosition();
            
            if (swipeDelta.magnitude > MIN_SWIPE)
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    // swipe left / right
                    swipes[(int) Directions.Left] = swipeDelta.x > 0;
                    swipes[(int) Directions.Right] = swipeDelta.x < 0;
                }
                else
                {
                    // swipe up / down
                    swipes[(int) Directions.Down] = swipeDelta.y > 0;
                    swipes[(int) Directions.Up] = swipeDelta.y < 0;
                }
                SendSwipe();
            }
            
        }

   
    }

    private void SendSwipe()
    {
        if(swipes[0] || swipes[1] || swipes[2] || swipes[3]) 
        {
            swipeEvent.Invoke();
        }
        // else // иначе тач
        // {
        //     touchEvent.Invoke();
        // }
        Reset();
    }

    private void Reset()
    {
        startTouchPos = swipeDelta = Vector2.zero;
        isSwipeStarted = false;
        
        for (int i = 0; i < swipes.Length; i++)
        {
            swipes[i] = false;
        }
    }
}
