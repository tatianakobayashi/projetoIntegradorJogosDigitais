using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NpcCapybara : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float delayTime = 1.5f;

    private Vector3 initialPosition;
    private bool movingToTarget = true;
    private bool isDelay = false;
    private float delayTimer = 0f;

    private Animator animator;
    


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDelay)
        {
            delayTimer -= Time.deltaTime;
            if(delayTimer < 0f)
            {
                isDelay = false;
                delayTimer = 0f;
            }
            else
            {
                return;
            }
        }

        if (movingToTarget)
        {
            MoveTowardTarget();
        }
        else
        {
            MoveBackToInitial();
        }
    }

    void MoveTowardTarget()
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        transform.Translate(targetDirection * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            movingToTarget =false;
            animator.SetTrigger("Idle");
            StartDelay();
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void MoveBackToInitial()
    {
        Vector3 initialDirection = (initialPosition - transform.position).normalized;
        transform.Translate(initialDirection * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
        {
            movingToTarget = true;
            animator.SetTrigger("Idle");
            StartDelay();
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void StartDelay()
    {
        isDelay = true;
        delayTimer = delayTime;
    }
}
