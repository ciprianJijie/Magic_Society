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

    public class NoFileFound : Exception
    {
        public NoFileFound(string filePath)
            :base ("File " + filePath + " can't be found. Does it exist?")
        {

        }
    }

    public class FailedToParseJSON : Exception
    {
        public FailedToParseJSON(string filePath)
            : base("Failed to parse JSON file " + filePath)
        {

        }
    }
}
