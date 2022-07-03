using UnityEngine;

[CreateAssetMenu]
public class Job : MonoBehaviour
{
	[SerializeField] private float duration;
	[SerializeField] private int cashReturn;
	[SerializeField] private JobManager manager;
	[SerializeField] private JobType jobType;
	[SerializeField] private GameObject anim;

	public JobType JobType => jobType;

	public Human human;

	public float Duration => duration;
	public int CashReturn => cashReturn;

	public void SetHuman(Human h)
	{
		if (human)
		{
			human.GetComponent<HumanStateMachine>().onStateChanged -= HandleStateChange;

		}
		human = h;
		human.GetComponent<HumanStateMachine>().onStateChanged += HandleStateChange;
	}

	private void HandleStateChange(HumanStateMachine.State prev, HumanStateMachine.State next)
	{
		if (human)
		{
			human.GetComponent<HumanStateMachine>().onStateChanged -= HandleStateChange;
		}
		Instantiate(anim, transform);
		human = null;
	}

	private void Awake()
	{
		manager.Add(this);
	}

	private void OnDestroy()
	{
		manager.Remove(this);
		if (human)
		{
			human.GetComponent<HumanStateMachine>().onStateChanged -= HandleStateChange;

		}
	}
}