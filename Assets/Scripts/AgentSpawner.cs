using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent AgentPrefab;
    [SerializeField]
    private int AgentsToSpawn = 25;
    [SerializeField]
    private Transform TargetLocation;
    
    private NavMeshTriangulation Triangulation;
    private ObjectPool<NavMeshAgent> AgentPool;

    private void Awake()
    {
        AgentPool = new ObjectPool<NavMeshAgent>(CreateAgent, OnGetAgent, OnReleaseAgent);
        Triangulation = NavMesh.CalculateTriangulation();
    }

    private NavMeshAgent CreateAgent()
    {
        NavMeshAgent agent = Instantiate(AgentPrefab);

        return agent;
    }

    private void OnGetAgent(NavMeshAgent Agent)
    {
        int index = Random.Range(1, Triangulation.vertices.Length);
        float lerpFactor = Random.value;
        Vector3 targetSpawnPoint = Vector3.Lerp(
                    Triangulation.vertices[index],
                    Triangulation.vertices[index - 1],
                    lerpFactor
                );
        

        if (NavMesh.SamplePosition(
                targetSpawnPoint,
                out NavMeshHit hit,
                2f,
                Agent.areaMask
            ))
        {
            Agent.transform.position = hit.position;
            Agent.Warp(hit.position);
        }
        else
        {
            Debug.LogWarning("Unable to generate valid location on NavMesh for agent! Picking a Triangulation Vertex");
            Agent.transform.position = Triangulation.vertices[index];
        }
        Agent.enabled = true;
    }

    private void OnReleaseAgent(NavMeshAgent Agent)
    {
        Agent.enabled = false;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < AgentsToSpawn; i++)
        {
            NavMeshAgent agent = AgentPool.Get();
            agent.SetDestination(TargetLocation.position);
            yield return null;
        }
    }
}
