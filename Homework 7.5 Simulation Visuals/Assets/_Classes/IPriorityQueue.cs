using System.Collections.Generic;

public interface IPriorityQueue<T> where T : IPrioritizable
{
	void Enqueue(T item);
	T Dequeue ();
	int Count { get; }
}
