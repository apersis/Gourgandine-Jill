using UnityEngine;
using UnityEngine.AI;
using System;

public class GourgandineMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //radius of sphere
    private Animator animator;
    private bool isPaused = false;
    private float pauseTime = 0f;
    private float pauseDuration = 0f;
    public TextMesh textMesh;
    public GameObject choux;
    private int compteurChoux = 0;
    public GameObject explosion;
    public AudioClip audioclip;
    private AudioSource audioSource;

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioclip;
    }

    void Update()
    {
        if (!isPaused && agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                // Mettre la pause ici
                animator.SetTrigger("idle");
                PauseAgent();
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
                animator.SetTrigger("walk");
            }
        }
        else if (isPaused)
        {
            // L'agent est en pause
            pauseTime += Time.deltaTime;

            if (pauseTime >= pauseDuration)
            {
                // Réactive l'agent après la pause
                isPaused = false;
                // Réinitialise le temps de pause
                pauseTime = 0f;
            }
        }
    }

    void PauseAgent()
    {
        // Générer une durée de pause aléatoire entre 2 et 10 secondes
        pauseDuration = UnityEngine.Random.Range(2f, 10f);
        // Mettre en pause l'agent
        isPaused = true;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    void OnMouseDown()
    {
        Outline outline = choux.GetComponent<Outline>();
        if(outline.enabled == true){
            compteurChoux++;
            int total = Int32.Parse(textMesh.text) - 1;
            textMesh.text = total.ToString();
            agent.SetDestination(agent.transform.position);
            pauseDuration = 15f;
            isPaused = true;
            if (compteurChoux > 10){
                explosion.transform.position = agent.transform.position;
                explosion.transform.localScale = new Vector3(2f,2f,2f);
                gameObject.transform.localScale = new Vector3(0,0,0);
                audioSource.Play();
            }
        }
    }
}