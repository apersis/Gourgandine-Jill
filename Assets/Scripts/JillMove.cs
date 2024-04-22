using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class JillMove : MonoBehaviour
{
    private Animator animator;
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject choux;

    public bool ismoving;
  
    public float clickPointTreshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Outline outline = choux.GetComponent<Outline>();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != choux)
                {
                    agent.SetDestination(hit.point);
                    animator.SetTrigger("run");
                    ismoving = true;
                }
     
            }
        }
        if(ismoving && agent.transform.position == agent.destination)
        {
            ismoving = false;
            animator.SetTrigger("idle");
        }

    }
}