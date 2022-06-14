using UnityEngine;

[CreateAssetMenu]
public class Job : MonoBehaviour
{
	[SerializeField] private float duration;
	[SerializeField] private int cashReturn;
	[SerializeField] private JobManager manager;
	[SerializeField] private JobType jobType;

	public JobType JobType => jobType;

	private void Awake()
	{
		manager.Add(this);
	}

	private void OnDestroy()
	{
		manager.Remove(this);
	}
}