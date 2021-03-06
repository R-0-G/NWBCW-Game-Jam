using UnityEngine;

// go to job, wait until job is finished then go home 

public class HumanAIController : MonoBehaviour
{
	[SerializeField] private HumanStateMachine stateMachine;
	[SerializeField] private TransformGroupManager doors;
	[SerializeField] private TransformGroupManager breakPoints;
	[SerializeField] private JobManager jobs;
	[SerializeField] private float breakTime = 25f;
	[SerializeField] private float autonomyTick = 5f;
	[SerializeField] private Human human;
	[SerializeField] private GameObject anim;


	private Job currentJob;
	private Transform breakTarget;
	private Transform friendTarget;
	private Transform doorTarget;

	public Transform JobTransform => currentJob.transform;

	public delegate void HumanEvent();
	public event HumanEvent CompletedJob;

	private void Awake()
	{
		stateMachine.onStateChanged += HandleStateChange;
	}

	private void OnDestroy()
	{
		stateMachine.onStateChanged -= HandleStateChange;
	}

	private void JobComplete()
	{
		if (CompletedJob != null)
		{
			CompletedJob();
			Instantiate(anim, currentJob.transform);
			Invoke("TryToActAutonomously", autonomyTick);
		}
	}

	private void ReturnToJob()
	{
		if (stateMachine.CurrentState != HumanStateMachine.State.DESTROYING || stateMachine.CurrentState != HumanStateMachine.State.GOING_HOME)
			stateMachine.TriggerStateChange(HumanStateMachine.State.FINDING_JOB);
	}

	private void TryToActAutonomously()
	{

	}

	private void HandleStateChange(HumanStateMachine.State prev, HumanStateMachine.State next)
	{
		CancelInvoke();
		switch (next)
		{
			case HumanStateMachine.State.FINDING_JOB:
				currentJob = jobs.GetRandom(true);
				if (currentJob == null)
				{
					stateMachine.TriggerStateChange(HumanStateMachine.State.SLACKING);
					Invoke("ReturnToJob", 1f);
				}
				else
				{
					currentJob.SetHuman(human);
				}
				break;
			case HumanStateMachine.State.WORKING:
				Invoke("JobComplete", currentJob.Duration);
				break;
			case HumanStateMachine.State.ON_BREAK:
				breakTarget = breakPoints.GetRandom();
				Invoke("ReturnToJob", breakTime);
				break;

			case HumanStateMachine.State.SLACKING:
				return;
			// return friendTarget.position;

			case HumanStateMachine.State.GOING_HOME:
				doorTarget = doors.GetRandom();
				return;

		}
	}

	public Vector3 GetTarget()
	{
		switch (stateMachine.CurrentState)
		{
			case HumanStateMachine.State.FINDING_JOB:
			case HumanStateMachine.State.AUTONOMOUS:
			case HumanStateMachine.State.UNIONIZING:
			case HumanStateMachine.State.WORKING:
				return JobTransform.position;

			case HumanStateMachine.State.ON_BREAK:
				return breakTarget.position;

			case HumanStateMachine.State.SLACKING:
				return friendTarget ? friendTarget.position : transform.position;

			case HumanStateMachine.State.GOING_HOME:
				return doorTarget.position;

		}

		return transform.position;
	}
}