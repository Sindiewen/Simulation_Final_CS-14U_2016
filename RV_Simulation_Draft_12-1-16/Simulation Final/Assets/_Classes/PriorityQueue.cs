// using System;
using System.Collections.Generic;
using System.Diagnostics;

public class PriorityQueue<T> : IPriorityQueue<T> where T : IPrioritizable
{
	private SortedDictionary<int, Queue<T>> queuesByPriority;
	private int count;

	public PriorityQueue()
	{
		queuesByPriority = new SortedDictionary<int, Queue<T>>();
		count = 0;
	}

	public void Enqueue(T item)
	{
		if (item == null) // Object.Equals(item, default(T)))
		{
			UnityEngine.Debug.LogWarning ("Received null item in PriorityQueue.Enqueue(...). Ignoring.");
			return;
		}
		
		Queue<T> queue;
		if (queuesByPriority.ContainsKey(item.Priority))
		{
			queue = queuesByPriority[item.Priority];
			Debug.Assert (queue != null, "Null subqueue in PriorityQueue.Enqueue(...).");
		}
		else
		{
			queue = new Queue<T>();
			queuesByPriority.Add(item.Priority, queue);
		}
		queue.Enqueue(item);
		count++;
	}

	public T Dequeue ()
	{
		T topPriorityItem = default(T);
		bool foundMatch = false;

		while (!foundMatch && queuesByPriority.Count > 0)
		{
			// get the value of the top priority (i.e. lowest priority value)
			int topPriorityValue = 0;
			foreach (int priority in queuesByPriority.Keys)
			{
				topPriorityValue = priority;
				break;
			}

			Queue<T> topPriorityQueue = queuesByPriority [topPriorityValue];
			Debug.Assert (topPriorityQueue != null, "Null topPriorityItemQueue in PriorityQueue.Dequeue(...).");

			// go through the queue of items and get the first item whose priority matches the current topPriorityValue
			while (topPriorityQueue.Count > 0)
			{
				topPriorityItem = topPriorityQueue.Dequeue();
				Debug.Assert (topPriorityItem != null, "Null topPriorityItem in PriorityQueue.Dequeue(...).");

				count--;

				if (topPriorityItem.Priority != topPriorityValue)
				{
					// stale data, just throw it away and look at the next item in this queue
					continue;
				}

				// good data, exit the loop
				foundMatch = true;
				break;
			}

			// if we have emptied the internal queue then remove it
			if (topPriorityQueue.Count == 0)
			{
				queuesByPriority.Remove (topPriorityValue);
			}
		}

		return (foundMatch ? topPriorityItem : default(T));
	}

	public int Count
	{
		get { return count; }
	}

	public void Clear()
	{
		foreach (Queue<T> queue in queuesByPriority.Values)
		{
			queue.Clear ();
		}
		queuesByPriority.Clear ();
	}
}










