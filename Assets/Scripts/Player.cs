using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using DG;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float lineChangeTime;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float pushingSpeed;
    [SerializeField] private Rigidbody rb;
    private float movementSpeed;
    private int currentLine = 0;

    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro weightText;
    private bool isPushing;



    [Header("Body Parts")] 
    [SerializeField] private Transform head;
    [SerializeField] private Transform chest;
    [SerializeField] private Transform rightArm;
    [SerializeField] private Transform leftArm;
    [SerializeField] private Transform pelvis; // таз
    [SerializeField] private Transform pelvis2;
    
    [Header("Gain/Lose Weight")]
    [SerializeField] private Vector3 headGrowValue;
    [SerializeField] private Vector3 chestGrowValue;
    [SerializeField] private Vector3 armsGrowValue;
    [SerializeField] private Vector3 legsGrowValue;
    [SerializeField] private Vector3 headLoseValue;
    [SerializeField] private Vector3 chestLoseValue;
    [SerializeField] private Vector3 armsLoseValue;
    [SerializeField] private Vector3 legsLoseValue;

    [Header("Weight")]
    [SerializeField] private float weight;
    [SerializeField] private float minWeight;
    [SerializeField] private float maxWeight;
    [SerializeField] private float loseWeightSpeed;
    [SerializeField] private float foodWeight;
    
    [Header("Other")]
    public UnityEvent finishEvent;
    public UnityEvent dieEvent;
    
    void Start()
    {
        animator.SetBool("isIdle", false);
        movementSpeed = runningSpeed;
    }

    
    void Update()
    {
        CheckWeight();
        rb.velocity = Vector3.forward * movementSpeed;
    }

    private void CheckWeight()
    {
        if (weight >= 75)
        {
            weightText.text = "Fat";
        }
        else if (weight <= 25)
        {
            weightText.text = "Skinny";
        }
        else
        {
            weightText.text = "Normal";
        }
    }

    public void ChangeLine()
    {
        if(!isPushing)
        {
            if(SwipeManager.instance.swipes[0] && currentLine != 0) // swipe left
            {
                currentLine--;
                rb.DOMoveX(currentLine * 3, lineChangeTime);
            }
            else if(SwipeManager.instance.swipes[1] && currentLine != 3) // swipe right
            {
                currentLine++;
                rb.DOMoveX(currentLine * 3, lineChangeTime);
            }
        }
    }

    void GainWeight()
    {
        if (weight < maxWeight)
        {
            head.localScale += headGrowValue; 
            chest.localScale += chestGrowValue;
            rightArm.localScale += armsGrowValue;
            leftArm.localScale += armsGrowValue;
            pelvis.localScale += legsGrowValue;
            pelvis2.localScale += legsGrowValue;
            weight += foodWeight;
        }
    }

    IEnumerator LoseWeight()
    {
        while(isPushing)
        {
            if (weight <= minWeight)
            {
                runningSpeed = 0;
                pushingSpeed = 0;
                movementSpeed = 0;
                animator.SetBool("isIdle", true);
                dieEvent.Invoke();
            }
            head.localScale -= headLoseValue * Time.deltaTime; 
            chest.localScale -= chestLoseValue * Time.deltaTime;
            rightArm.localScale -= armsLoseValue * Time.deltaTime;
            leftArm.localScale -= armsLoseValue * Time.deltaTime;
            pelvis.localScale -= legsLoseValue * Time.deltaTime;
            pelvis2.localScale -= legsLoseValue * Time.deltaTime;
            weight -= loseWeightSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private void WallPushing()
    {
        isPushing = true;
        animator.SetBool("isPushing", true);
        movementSpeed = pushingSpeed;
        StartCoroutine(LoseWeight());
    }

    private void StopWallPushing()
    {
        animator.SetBool("isPushing", false);
        movementSpeed = runningSpeed;
        StopCoroutine(LoseWeight());
        isPushing = false;
    }

    private void Finish()
    {
        movementSpeed = 0;
        animator.SetBool("isIdle", true);
        finishEvent.Invoke();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            WallPushing();
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            StopWallPushing();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            GainWeight();
        }
        if(other.CompareTag("Finish"))
        {
            Finish();
        }
    }
}
