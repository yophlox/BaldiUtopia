using UnityEngine;

public class FacultyTriggerScript : MonoBehaviour
{
	public PlayerScript ps;

	private BoxCollider hitBox;

	private void Start()
	{
		hitBox = GetComponent<BoxCollider>();
	}

	private void Update()
	{
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			ps.ResetGuilt("faculty", 2f);
		}
	}
}
