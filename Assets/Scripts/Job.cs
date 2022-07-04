using UnityEngine;

[CreateAssetMenu]
public class Job : MonoBehaviour
{
	[SerializeField] private float duration;
	[SerializeField] private int cashReturn;
	[SerializeField] private JobManager manager;
	[SerializeField] private JobType jobType;

	public Color activecol;
	public Color inactivecol;

	public JobType JobType => jobType;

	public SpriteRenderer sp;

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
		sp.color = activecol;
		human.GetComponent<HumanStateMachine>().onStateChanged += HandleStateChange;
	}

	private void HandleStateChange(HumanStateMachine.State prev, HumanStateMachine.State next)
	{
		if (human)
		{
			human.GetComponent<HumanStateMachine>().onStateChanged -= HandleStateChange;
		}

		sp.color = inactivecol;
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