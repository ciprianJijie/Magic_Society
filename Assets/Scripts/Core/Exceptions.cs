using System;

namespace MS
{
	public class NoInstance : Exception
	{
		public NoInstance(object obj)
			: base ("No instance initialized for " + obj)
		{
		}
	}
	
	public class AlreadyInstantiated : Exception
	{
		public AlreadyInstantiated(object obj)
			: base ("Singleton " + obj + " already instantiated")
		{
		}
	}
}
