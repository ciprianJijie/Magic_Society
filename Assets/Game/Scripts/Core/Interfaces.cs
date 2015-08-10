using System;
using UnityEngine;

namespace MS
{
    // Events
    public delegate void ObjectEvent(GameObject obj);
    public delegate void ViewEvent(IUpdatableView view);
    public delegate void ViewEvent<T>(IUpdatableView<T> view) where T : MS.ModelElement;

    // Interfaces

    public interface IParseable
    {
        void FromJSON(SimpleJSON.JSONNode json);
        SimpleJSON.JSONNode ToJSON();
    }

    public interface IModelRelated<T> where T : MS.ModelElement
    {
        void BindTo(T element);
        T Model { get; }
    }

    public interface IObjectRelated
    {
        GameObject Object { get; }
    }

    public interface IUpdatableView
    {
        void UpdateView();
        bool IsViewOf(MS.ModelElement element);
        event ViewEvent OnDestroyed;
    }

    public interface IUpdatableView<T> : IModelRelated<T> where T : MS.ModelElement
    {
        void UpdateView(T element);
        bool IsViewOf(T element);
        event ViewEvent<T> OnDestroyed;
    }

    public interface IUpdatablePositionalView
    {
        void UpdateView(int x, int y);
        bool IsViewOf(MS.ModelElement element);
    }

    public interface IViewCreator<T> where T : MS.ModelElement
    {
        IUpdatableView<T> CreateView(T modelElement);
    }

}
