using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Defender : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject targetPlayer;
    [SerializeField] private float endLine;


    [SerializeField] private int defenderSpeedMin = 0;
    [SerializeField] private int defenderSpeedMax = 0;
    private float defenderSpeed = 0.0f;

    [SerializeField] private bool useMovementPrediction;
    [Range(-1,1)]
    [SerializeField] private float MovementPredictionThreshold = 0.0f;
    [Range(0.25f,2.0f)]
    [SerializeField] private float MovementPredictionTimed = 0.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        defenderSpeed = Random.Range(defenderSpeedMin, defenderSpeedMax);

        agent.speed = defenderSpeed;
    }

    void Update()
    {
        if(targetPlayer != null)
        {
            agent.SetDestination(CalculateApproach());
        }
    }

    Vector3 CalculateApproach()
    {
        Vector3 targetPosition = targetPlayer.transform.position - (Vector3.Normalize(targetPlayer.transform.position) * 1.5f);
        // float distanceToEnd = Vector3.Distance(targetPosition, new Vector3(targetPosition.x, targetPosition.y, endLine));
        float distanceToBlock = Vector3.Distance(targetPosition, new Vector3(targetPosition.x, targetPosition.y, endLine));

        float distToPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);

        //Applying pythagorean theorem to calculate Angle of Pursuit
        // float length = Mathf.Sqrt(Mathf.Pow(distanceToEnd, 2) + Mathf.Pow(distToPlayer, 2));

        float length = Mathf.Sqrt(Mathf.Pow(distanceToBlock, 2) + Mathf.Pow(distToPlayer, 2));

        // Vector3 pointOfInterception = new Vector3(-distToPlayer, transform.position.y, -distanceToEnd);
        Vector3 pointOfInterception = new Vector3(-distToPlayer, transform.position.y, -distanceToBlock);

        // return pointOfInterception;
        return targetPosition;
    }

    void deductionRadius()
    {
        if(Vector3.Distance(transform.position, targetPlayer.transform.position) > 5.0f)
        {
            
        }
    }
}
