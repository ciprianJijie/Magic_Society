using System;

public interface IParseable
{
	void FromJSON(SimpleJSON.JSONNode json);
	SimpleJSON.JSONNode ToJSON();
}
