using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGraphics : MonoBehaviour
{
	[SerializeField] private SpriteRenderer sp;
	[SerializeField] private Animator anim;
	[SerializeField] private Animator zanim;
	public float threshold = 0.2f;
	public Vector3 frd;

	private bool isZombie;

	private void Update()
	{
		sp.transform.up = Vector3.up;
		frd = transform.forward;
		anim.gameObject.SetActive(!isZombie);
		zanim.gameObject.SetActive(isZombie);
		if (isZombie)
		{
			if ((Mathf.Abs(transform.forward.x) < threshold) && transform.forward.y > 0)
			{
				zanim.Play("ZombieUp");
			}
			else if ((Mathf.Abs(transform.forward.x) < threshold) && transform.forward.y <= 0)
			{
				zanim.Play("ZombieDown");
			}
			else if (transform.forward.x > 0)
			{
				zanim.Play("ZombieRight");
			}
			else
			{
				zanim.Play("ZombieLeft");
			}

		}
		else
		{
			if ((Mathf.Abs(transform.forward.x) < threshold) && transform.forward.y > 0)
			{
				anim.Play("HumanUp");
			}
			else if ((Mathf.Abs(transform.forward.x) < threshold) && transform.forward.y <= 0)
			{
				anim.Play("HumanDown");
			}
			else if (transform.forward.x > 0)
			{
				anim.Play("HumanRight");
			}
			else
			{
				anim.Play("HumanLeft");
			}

		}
	}
	public void Ungroup()
	{
		// sp.color = new Color(0f, 0f, 0f, 1f);
	}

	public void SetActive(float t)
	{
		float value = Mathf.InverseLerp(0f, 1f, sp.color.r);
		float lerpValue = t;
		if (value < lerpValue)
		{
			sp.color = new Color(lerpValue, 0f, 0f, 1f);
		}
	}

	public void ConfigureZombie()
	{
		isZombie = true;
		// sp.sprite = zombSp;
	}
}
