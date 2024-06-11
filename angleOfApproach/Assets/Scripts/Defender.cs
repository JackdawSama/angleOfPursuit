using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Defender : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject targetPlayer;
    [SerializeField] private float endLine;


    [SerializeField] private float defenderSpeedMin = 0.0f;
    [SerializeField] private float defenderSpeedMax = 0.0f;
    private float defenderSpeed = 0.0f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
    }

    void CalculateApproach()
    {
        Vector3 targetPosition = targetPlayer.transform.position + (Vector3.Normalize(targetPlayer.transform.position) * 1.5f);
        float distanceToEnd = Vector3.Distance(targetPosition, new Vector3(targetPosition.x, targetPosition.y, endLine));
        float distToPlayer = Vector3.Distance(targetPosition, transform.position);

        //Applying pythagorean theorem to calculate Angle of Pursuit
        float length = Mathf.Sqrt(Mathf.Pow(distanceToEnd, 2) + Mathf.Pow(distToPlayer, 2));
    }
}
