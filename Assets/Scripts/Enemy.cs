using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    [SerializeField]private int targetIndex;
    private int TargetIndex
    {
        get
        {
            return targetIndex;
        }
        set
        {
            if (value >= waypoints.Count)
            {
                targetIndex = 0;
            }
            else
            {
                targetIndex = value;
            }
            targetPosition = waypoints[targetIndex].position;
        }
    }
    [SerializeField]private Vector3 targetPosition;
    [SerializeField]private Vector2 moveVector;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        TargetIndex = 0;
    }
    private void Update()
    {
        if (Mathf.Abs(transform.position.x - targetPosition.x) < .25f)
        {
            TargetIndex++;
        }
        if (transform.position.x > targetPosition.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
    private void FixedUpdate()
    {
        float inputX = targetPosition.x - transform.position.x;
        moveVector = new Vector2(inputX,0).normalized;
        rb.MovePosition(rb.position + moveVector * Time.fixedDeltaTime);
    }
}
