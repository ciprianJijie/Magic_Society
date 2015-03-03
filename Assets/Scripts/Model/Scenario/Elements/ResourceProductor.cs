using UnityEngine;
using System.Collections;
using SimpleJSON;

namespace MS.Model
{
    public class ResourceProducer : MS.Model.MapElement
    {
        public Player               Owner;

        public GameResource        Resource;
        public int                 Production;


        public ResourceProducer(int x, int y, GameResource resource, int production)
            : base(x, y)
        {
            Resource    =   resource;
            Production  =   production;

            MS.Manager.TurnManager.OnTurnStarted += OnTurnStarted;
        }

        public ResourceProducer(JSONNode json)
            : base(json["location"]["x"].AsInt, json["location"]["y"].AsInt)
        {
            FromJSON(json);
            MS.Manager.TurnManager.OnTurnStarted += OnTurnStarted;
        }

        public void OnTurnStarted(Player player, int turnCount)
        {
            if (player == Owner)
            {
                Owner.AddResource(Resource.Name, Production);
            }
        }

        public override void FromJSON(JSONNode json)
        {
            Resource    =   Manager.GameManager.Game.Scenario.Map.GetResource(json["resource"]);
            Production  =   json["production"].AsInt;
            Owner       =   Manager.GameManager.GetPlayer(json["owner"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json                    =   new JSONNode();
            json["location"]["x"]   =   Location.x.ToString();
            json["location"]["y"]   =   Location.y.ToString();
            json["resource"]        =   Resource.Name;
            json["production"]      =   Production.ToString();

            return json;
        }

        public void PrepareDelete()
        {
            MS.Manager.TurnManager.OnTurnStarted -= OnTurnStarted;
        }

    }
}
