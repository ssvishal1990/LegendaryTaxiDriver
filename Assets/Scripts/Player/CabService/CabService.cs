using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CabService : MonoBehaviour
{
    [SerializeField] private bool passengerOnBoard;
    [SerializeField] private int currentNoOfPassengers;
    [SerializeField] private int maxNoOfPassengers;
    [SerializeField] private float circleRadius;
    [SerializeField] private LayerMask destination;

    private void Update()
    {
        checkIfPassengerOnBoard();
    }

    private void checkIfPassengerOnBoard()
    {
        if (currentNoOfPassengers != 0)
        {
            passengerOnBoard = true;
        }else
        {
            passengerOnBoard = false;
        }
    }

    public void boardingPassenger()
    {
        if (currentNoOfPassengers < maxNoOfPassengers)
        {
            currentNoOfPassengers++;
        }
        Debug.Log("Maximum passengers on boarded");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public Collider2D checkIfDestinationIsNear()
    {
        Debug.Log("Inside destination is near");
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, circleRadius, destination);
        foreach (Collider2D hit in hits)
        {
            Debug.Log(hit.gameObject.tag);
            if (hit.gameObject.tag == "Destination")
            {
                return hit;
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

    public void dropPassenger()
    {
        // Logic yet to be implemented
    }
}

