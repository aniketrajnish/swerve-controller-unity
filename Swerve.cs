using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swerve : MonoBehaviour
{
    [SerializeField] private float swipeAmount = 5f;
    [SerializeField] private float swipeBound = 1f;
    [SerializeField] private float forwardSpeed = 1f;
    [SerializeField] private float levelBounds; 
    private float previousX;
    private float currentX;
    public float CurrentX => currentX;   
  

    public void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            previousX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            currentX = Input.mousePosition.x - previousX;
            previousX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentX = 0f;
        }
        
        transform.position = new Vector3(transform.position.x,
            transform.position.y, Mathf.Clamp(transform.position.z, -levelBounds, levelBounds));
            
        float factor = 30;
        float swipeSpeed = factor * swipeAmount * CurrentX;
        float forwardAmount = Time.deltaTime * forwardSpeed;
        swipeSpeed = Mathf.Clamp(swipeSpeed, -swipeBound * factor, swipeBound * factor);
        
        //translate
        if (Input.GetMouseButton(0))
                transform.Translate(forwardAmount, 0, -swipeSpeed * Time.deltaTime);
        
        //look
        Vector3 movement = new Vector3(0, swipeSpeed * 3f*Time.deltaTime*factor/5, 0);
        if (swipeSpeed != 0)
        {
            transform.Rotate(movement);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), 2f * Time.deltaTime);
        }
    }
}
