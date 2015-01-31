using System;

namespace MS.Exceptions
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

    public class PlayerNotFound : Exception
    {
        public PlayerNotFound(string playerName)
            : base ("No player named " + playerName + " exists in the current game")
        {

        }
    }

    public class FactoryMethodWrongType : Exception
    {
        public FactoryMethodWrongType(Type classType)
            : base("Factory method can't create an object of class " + classType)
        {

        }

        public FactoryMethodWrongType(string typeName)
            : base("Factory method can't create an object of class " + typeName)
        {

        }
    }

    public class WrongType : Exception
    {
        public WrongType(object obj, Type baseClass)
            : base("Object " + obj + " is not of type " + baseClass + " neither it derives from it.")
        {

        }
    }
}
