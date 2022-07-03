using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu]
public class JobManager : ScriptableObject
{
	public List<Job> jobs = new List<Job>();
	private Dictionary<JobType, List<Job>> jobDict = new Dictionary<JobType, List<Job>>();
	public void Add(Job job)
	{
		if (!jobs.Contains(job))
		{
			jobs.Add(job);
		}
	}

	public void Remove(Job job)
	{
		if (jobs.Contains(job))
		{
			jobs.Remove(job);
		}
	}

	public void BuildJobDict()
	{
		jobDict = jobs.GroupBy(x => x.JobType).ToDictionary(x => x.Key, x => x.ToList());
	}

	public Job GetRandom(bool unassigned = false)
	{
		if (unassigned)
		{
			List<Job> availJobs = jobs.Where(x => !x.human).ToList();
			int index = Random.Range(0, availJobs.Count);
			if (index >= availJobs.Count)
			{
				index = availJobs.Count - 1;
			}
			if (availJobs.Count == 0)
			{
				return null;
			}
			return availJobs[index];
		}
		return jobs[Random.Range(0, jobs.Count)];
	}

	public Job GetRandom(JobType type)
	{
		return jobDict[type][Random.Range(0, jobDict[type].Count)];
	}


	// public Job GetJob(JobType profession, float autonomy)
	// {
	// 	if (Random.value <= autonomy)
	//     {
	//         return jobs[Random.Range(0, jobs.Count-1)];
	//     }
	//     else{
	//         return jobDict[profession]
	//     }
	// }
}