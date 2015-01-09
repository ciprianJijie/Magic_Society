using System;

namespace MS
{
	public interface IParseable
	{
		void FromJSON(SimpleJSON.JSONNode json);
		SimpleJSON.JSONNode ToJSON();
	}
}


