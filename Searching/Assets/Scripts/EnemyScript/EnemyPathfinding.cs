using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] Transform playerTarget;
    [SerializeField] GameObject currentWaypoint;
    List<GameObject> nodeChoices = new List<GameObject>();
    List<GameObject> nodesTravelled = new List<GameObject>();
    EnemyFieldOfView enemyFOV;
    float enemySpeed = 3f;
    Rigidbody rb;
    Animator enemyAnimator;
    GameObject nextNode;
    private NavMeshAgent nMA;
    int nextNodeInt;
    EnemyView enemyView;
    void Start()
    {
        enemyView = GetComponent<EnemyView>();
        rb = GetComponent<Rigidbody>();
        enemyFOV = GetComponent<EnemyFieldOfView>();
        nMA = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        nMA.speed = enemySpeed;
        Transform waypointTransform = currentWaypoint.transform;
        if (Vector3.Distance(transform.position, waypointTransform.position) < 1)
        {
            nodesTravelled.Add(currentWaypoint);
            currentWaypoint = FindNextWayPoint();
        }
        if (enemyFOV.playerVisible == true)
        {
            nMA.SetDestination(playerTarget.position);
            //nMA.speed = 7;
            
        }
        else if (enemyFOV.playerVisible == false)
        {   
            nMA.SetDestination(waypointTransform.position);
            //nMA.speed = 3;
        }
    }

    GameObject FindNextWayPoint()
    {
        int nodeAmount = nodesTravelled.Count;
        WaypointDetection waypointNodes = currentWaypoint.GetComponent<WaypointDetection>();
        foreach (GameObject node in waypointNodes.nearbyWaypoints)
        {
            if (nodeAmount > 1)
            {
                if ((nodesTravelled[nodeAmount - 2] != node))
                {
                    nodeChoices.Add(node);
                }
            }
            else
            {
                nodeChoices.Add(node);
            }
        }

        nextNodeInt = Random.Range(0, nodeChoices.Count);
        nextNode = nodeChoices[nextNodeInt];
        nodeChoices.Clear();
        return nextNode;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, playerTarget.position, out hit))
            {
                if (hit.collider.gameObject.name == "Player" && enemyView.isVisible == true)
                {
                    enemySpeed = 0f;
                    rb.MoveRotation(Quaternion.LookRotation(playerTarget.transform.position - transform.position));
                    enemyAnimator.SetTrigger("Idling");
                    nMA.velocity = new Vector3(0, 0, 0);
                }
                else
                {
                    enemySpeed = 3f;
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyAnimator.SetTrigger("Moving");
            enemySpeed = 3f;
        }
    }

}
