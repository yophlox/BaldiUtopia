using UnityEngine;
using UnityEngine.AI;

public class SweepScript : MonoBehaviour
{
	public Transform wanderTarget;

	public AILocationSelectorScript wanderer;

	public float coolDown;

	public float waitTime;

	public int wanders;

	public bool active;

	private Vector3 origin;

	public AudioClip aud_Sweep;

	public AudioClip aud_Intro;

	private NavMeshAgent agent;

	private AudioSource audioDevice;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		audioDevice = GetComponent<AudioSource>();
		origin = base.transform.position;
		waitTime = Random.Range(120f, 180f);
	}

	private void Update()
	{
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
		if (waitTime > 0f)
		{
			waitTime -= Time.deltaTime;
		}
		else if (!active)
		{
			active = true;
			wanders = 0;
			Wander();
			audioDevice.PlayOneShot(aud_Intro);
		}
	}

	private void FixedUpdate()
	{
		if ((agent.velocity.magnitude <= 1f) & (coolDown <= 0f) & (wanders < 5) & active)
		{
			Wander();
		}
		else if (wanders >= 5)
		{
			GoHome();
		}
	}

	private void Wander()
	{
		wanderer.GetNewTargetHallway();
		agent.SetDestination(wanderTarget.position);
		coolDown = 1f;
		wanders++;
	}

	private void GoHome()
	{
		agent.SetDestination(origin);
		waitTime = Random.Range(120f, 180f);
		wanders = 0;
		active = false;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "NPC")
		{
			NavMeshAgent component = other.GetComponent<NavMeshAgent>();
			component.velocity = agent.velocity + component.velocity * 0.2f;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "NPC" || other.tag == "Player")
		{
			audioDevice.PlayOneShot(aud_Sweep);
		}
	}
}
