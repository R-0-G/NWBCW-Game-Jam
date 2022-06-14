using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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