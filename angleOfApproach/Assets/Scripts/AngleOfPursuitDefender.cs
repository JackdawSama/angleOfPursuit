using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AngleOfPursuitDefender : MonoBehaviour
{   
    //References
    [SerializeField] NavMeshAgent agent;
    [SerializeField] PlayerMove player;
    [SerializeField] CharacterController playerCC;

    [SerializeField] private float userDefinedDefenderSpeed = 13;
    [SerializeField] private bool randomSpeed = false;
    [SerializeField] private float defenderSpeedMin = 0;
    [SerializeField] private float defenderSpeedMax = 0;
    [SerializeField] private bool canIntercept = true;

    private float timeToIntercept1;
    private float timeToIntercept2;
    private float timeToIntercept;

    private Vector3 pointOfIntersection;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(player == null)
        {
            player = FindObjectOfType<PlayerMove>();
            playerCC = player.GetComponent<CharacterController>();
        }

        //Sets Defender's Speed based on user's options
        if(randomSpeed)
        {
            agent.speed = Random.Range(defenderSpeedMin, defenderSpeedMax);
        }
        else
        {
            agent.speed = userDefinedDefenderSpeed;
        }
    }

    void Update()
    {
        if(player != null)
        {
            CalculateQuadEqn();
            if(canIntercept)
            {
                CalculatePointOfIntersectionAndDefenderVelocity();
            }
            else
            {
                agent.SetDestination(player.transform.position);
            }
        }
    }

    void CalculateQuadEqn()
    {
        //Variables for calculation
        Vector3 PosVectorPlayertoDefender = transform.position - player.transform.position;
        float DistancePlayerToDefender = PosVectorPlayertoDefender.magnitude;

        //Evaluation of the quadratic equation's discriminant
        float quadDeterminant = 4 * (Mathf.Pow(Vector3.Dot(PosVectorPlayertoDefender, playerCC.velocity),2)
                                + (Mathf.Pow(agent.speed, 2) - Mathf.Pow(playerCC.velocity.magnitude, 2)) * Mathf.Pow(DistancePlayerToDefender,2));

        if(quadDeterminant < 0.0f)
        {
            //If the determinant is negative, the quadratic equation has no real solution. So can't intercept.
            canIntercept = false;
        }
        else if(quadDeterminant == 0.0f)
        {
            //If the determinant is zero, the quadratic equation has only one real solution.
            timeToIntercept = -2 * Vector3.Dot(PosVectorPlayertoDefender, playerCC.velocity)
                                /(2 * (Mathf.Pow(agent.speed, 2) - Mathf.Pow(playerCC.velocity.magnitude, 2)));

            //If the time to intercept is negative then the defender can't intercept
            if(timeToIntercept < 0.0f)
            {
                canIntercept = false;
            }
            else
            {
                canIntercept = true;
            }
        }
        else
        {
            //If the determinant is positive, the quadratic equation has two real solutions.
            //evalutes the two possible times of interception
            timeToIntercept1 = (-2 * Vector3.Dot(PosVectorPlayertoDefender, playerCC.velocity)) + Mathf.Sqrt(quadDeterminant)
                                /(2 * (Mathf.Pow(agent.speed, 2) - Mathf.Pow(playerCC.velocity.magnitude, 2)));

            timeToIntercept2 = (-2 * Vector3.Dot(PosVectorPlayertoDefender, playerCC.velocity)) - Mathf.Sqrt(quadDeterminant)
                                /(2 * (Mathf.Pow(agent.speed, 2) - Mathf.Pow(playerCC.velocity.magnitude, 2)));
            
            //Checking for negative time results to filter out
            if(timeToIntercept1 < 0.0f && timeToIntercept2 < 0.0f)
            {
                canIntercept = false;
            }
            else if(timeToIntercept1 > 0.0f && timeToIntercept2 < 0.0f)
            {
                canIntercept = true;
                timeToIntercept = timeToIntercept1;
            }
            else if(timeToIntercept1 < 0.0f && timeToIntercept2 > 0.0f)
            {
                canIntercept = true;
                timeToIntercept = timeToIntercept2;
            }
            else if(timeToIntercept1 > 0.0f && timeToIntercept2 > 0.0f)
            {
                //In case both the solutions are positive, then the defender will choose the least time to intercept
                canIntercept = true;
                if(timeToIntercept1 > timeToIntercept2)
                {
                    timeToIntercept = timeToIntercept2;
                }
                else
                {
                    timeToIntercept = timeToIntercept1;
                }
            }
        }
    }

    void CalculatePointOfIntersectionAndDefenderVelocity()
    {
        //Calculating the point of intersection
        pointOfIntersection = player.transform.position + playerCC.velocity * timeToIntercept;

        //Calculating the defender's velocity
        Vector3 defenderVelocity = (pointOfIntersection - transform.position).normalized * agent.speed;
        agent.velocity = defenderVelocity;
        // agent.SetDestination(agent.velocity);
        agent.SetDestination(pointOfIntersection);
    }
    
}