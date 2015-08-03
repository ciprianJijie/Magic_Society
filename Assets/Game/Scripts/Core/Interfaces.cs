using System;

public interface IParseable
{
	void FromJSON(SimpleJSON.JSONNode json);
	SimpleJSON.JSONNode ToJSON();
}

public interface IUpdatableView
{
    void UpdateView();
}

public interface IUpdatablePositionalView
{
    void UpdateView(int x, int y);
}
