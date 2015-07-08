using System;

public class NoInstanceException : Exception
{
	public NoInstanceException(object obj)
		: base ("No instance initialized for " + obj)
	{
	}
}

public class AlreadyInstantiatedException : Exception
{
	public AlreadyInstantiatedException(object obj)
		: base ("Singleton " + obj + " already instantiated")
	{
	}
}

public class NoFileFoundException : Exception
{
    public NoFileFoundException(string filePath)
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

public class MissingPrefabForType : Exception
{
	public MissingPrefabForType(Type baseClass)
	: base ("No prefab is associated to visualize objects of type " + baseClass)
	{

	}
}

public class ResourceNotFound : Exception
{
	public ResourceNotFound(string name)
	: base ("No resource named " + name + " is registered for this map")
	{

	}
}
