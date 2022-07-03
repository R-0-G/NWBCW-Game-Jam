using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanStateMachine : MonoBehaviour
{
	public enum State { FINDING_JOB, WORKING, ON_BREAK, SLACKING, AUTONOMOUS, GOING_HOME, UNIONIZING, DESTROYING }

	[SerializeField] private State state;
	[SerializeField] private HumanColliderCheck colliderCheck;
	[SerializeField] private HumanAIController aiController;
	[SerializeField] private TimeManager timeManager;
	public State CurrentState => state;
	public delegate void StateChange(State previous, State next);
	public event StateChange onStateChanged;

	private void Awake()
	{
		aiController.CompletedJob += HandleCompletedJob;
		timeManager.OnBreak.AddListener(HandleOnBreak);
		timeManager.OnLunch.AddListener(HandleOnBreak);
		timeManager.OnNight.AddListener(HandleOnDayEnd);
	}
	private void OnDestroy()
	{
		aiController.CompletedJob -= HandleCompletedJob;
		timeManager.OnBreak.RemoveListener(HandleOnBreak);
		timeManager.OnLunch.RemoveListener(HandleOnBreak);
		timeManager.OnNight.RemoveListener(HandleOnDayEnd);
	}

	private void HandleOnDayEnd()
	{
		TriggerStateChange(State.GOING_HOME);
	}

	private void HandleOnBreak()
	{
		TriggerStateChange(State.ON_BREAK);
	}

	private void HandleCompletedJob()
	{
		TriggerStateChange(State.SLACKING);
	}

	private void Start()
	{
		TriggerStateChange(State.FINDING_JOB);
	}

	public void TriggerStateChange(State nextState)
	{
		if (onStateChanged != null)
		{
			State prevState = state;
			state = nextState;
			onStateChanged(prevState, nextState);
		}
	}

	private void Update()
	{
		switch (state)
		{
			case State.FINDING_JOB:
				FindJobUpdate();
				break;
			case State.ON_BREAK:
				OnBreakUpdate();
				break;
			case State.WORKING:
				OnWorkUpdate();
				break;
			case State.SLACKING:
				SlackingUpdate();
				break;
			case State.AUTONOMOUS:
				AutonomousUpdate();
				break;
			case State.GOING_HOME:
				GoingHomeUpdate();
				break;
			case State.UNIONIZING:
				UnionizingUpdate();
				break;
			default:
				return;
		}
	}

	private void FindJobUpdate()
	{
		if (colliderCheck.IsJobInRange(aiController.JobTransform))
		{
			TriggerStateChange(State.WORKING);
		}
	}
	private void OnWorkUpdate()
	{

	}


	private void OnBreakUpdate()
	{

	}

	private void SlackingUpdate()
	{

	}

	private void AutonomousUpdate()
	{

	}

	private void GoingHomeUpdate()
	{
		if (colliderCheck.AnyDoorInRange(out Transform door))
		{
			TriggerStateChange(State.DESTROYING);
		}
	}

	private void UnionizingUpdate()
	{

	}
}
