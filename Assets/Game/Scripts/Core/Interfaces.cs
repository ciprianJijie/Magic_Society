using System;
using System.Collections.Generic;
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

    public interface IOwnable
    {
        Model.Player Owner { get; set; }
    }

    public interface IGridPositioned
    {
        int GridX { get; set; }
        int GridY { get; set; }
    }

    public interface IEventListener
    {
        void SubscribeToEvents();
        void UnsubscribeToEvents();
    }

    public interface IResourceCollector
    {
        Model.ResourceAdvancedAmount Collect();
        int CalculateEstimatedFood();
        int CalculateEstimatedProduction();
        int CalculateEstimatedGold();
        int CalculateEstimatedResearch();
    }

    public interface IResourceWarehouse
    {
        void Store(Model.ResourceAmount amount);
        void Store(Model.ResourceAdvancedAmount amount);
        void ClearCollectedCache();
    }

    public interface IUpkeepMaintained
    {
        void PayUpkeepCosts();
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

    public interface IViewCreator
    {
        IUpdatableView  CreateView(ModelElement modelElement);
        bool            HasViewFor(ModelElement modelElement);
        IUpdatableView  FindView(ModelElement modelElement);
        void            DestroyView(ModelElement modelElement);
    }

    public interface IViewCreator<T> where T : MS.ModelElement
    {
        IUpdatableView<T>   CreateView(T modelElement);
        bool                HasViewFor(T modelElement);
        IUpdatableView<T>   FindView(T modelElement);
        void                DestroyView(T modelElement);
    }

    public interface IViewUpdater
    {
        void UpdateAllViews();
        void UpdateView(ModelElement element);
    }

    public interface IController : IViewCreator, IViewUpdater
    {

    }

    public interface IControllerCreator<T, R>
        where T: IViewCreator<R>
        where R: ModelElement
    {
        T CreateController<S>() where S: IUpdatableView<R>, IObjectRelated;
    }

    public interface IHouseOwneable
    {
        Model.NobleHouse ChiefHouse { get; set; }
    }

    public interface IRandomizable
    {
        void Randomize();
    }
}
