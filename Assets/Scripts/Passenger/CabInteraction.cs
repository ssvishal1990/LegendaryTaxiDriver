using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabInteraction : MonoBehaviour
{
    [SerializeField] float overlapCircleRadius = 2f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector2 moveTowardsCabSpeed;
    [SerializeField] bool cabOnBoarded = false;
    [SerializeField] bool destinationNear = false;
    [SerializeField] bool destinationReached = false;
    [SerializeField] Collider2D destinationGameObjectCollider;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        destinationReached = false;
        destinationGameObjectCollider = null;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (!destinationReached)
        {
            SearchForCab();
        }
        // This means we are inside the cab and on our way to destination
        CheckIfWeAreApproachingDestination();
        if (destinationNear)
        {
            OnNearDestinationMoveTowardsDestion(destinationGameObjectCollider);
        }

    }

    private void OnNearDestinationMoveTowardsDestion(Collider2D hit)
    {
        if (hit == null)
        {
            return;
        }
        if (destinationNear)
        {
            MoveTowardTheCollider(hit);
        }
    }

    private void CheckIfWeAreApproachingDestination()
    {
        if (!spriteRenderer.enabled && !destinationReached && !destinationNear)
        {
            CabService cabService = FindObjectOfType<CabService>();
            destinationGameObjectCollider = cabService.checkIfDestinationIsNear();
            if (destinationGameObjectCollider == null)
            {
                Debug.Log("no Hit Detected");
                transform.position = cabService.gameObject.transform.position;
                return;
            }
            if (cabService.gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                spriteRenderer.enabled = true;
                destinationNear = true;
            }

            
        }
    }

    private void SearchForCab()
    {
        Vector2 center = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, overlapCircleRadius, layerMask);
        foreach (Collider2D hit in hits)
        {
            SearchForPlayer(hit);
        }
    }

    private void SearchForPlayer(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            Debug.Log("Player Detected");
            Rigidbody2D cabrb = hit.gameObject.GetComponent<Rigidbody2D>();
            if (rb.velocity == Vector2.zero)
            {
                MoveTowardTheCollider(hit);
            }
        }
    }

    private void MoveTowardTheCollider(Collider2D hit)
    {
        Vector2 direction = (transform.position - hit.gameObject.transform.position) * -1;
        Vector2 moveValue = direction * Time.deltaTime * moveTowardsCabSpeed;

        Vector3 newPosition = new Vector3(transform.position.x + moveValue.x,
                                        transform.position.y + moveValue.y,
                                        transform.position.z);

        Debug.DrawLine(newPosition, transform.position, Color.green, Mathf.Infinity);
        rb.MovePosition(newPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, overlapCircleRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CabService cabService = collision.gameObject.GetComponent<CabService>();
            cabService.boardingPassenger();
            //gameObject.SetActive(false);
            //gameObject.transform = 
            spriteRenderer.enabled = false;
            
        }
    }
}
