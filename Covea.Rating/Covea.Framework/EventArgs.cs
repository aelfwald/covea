using System;

namespace Covea.Framework
{
	public class EventArgs<T> : EventArgs
	{
		public T Data { get; private set; }

		public static EventArgs<T> Create(T data)
		{
			return new EventArgs<T>(data);
		}

		public EventArgs(T data)
			: base()
		{
			Data = data;
		}
	}


}
