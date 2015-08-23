using UnityEngine;
using System.Collections.Generic;
using MS.Model;
using MS.Controllers.Editor;

namespace MS.Editor.UI
{
    public class PlayersSetupManager : MonoBehaviour
    {
        // =========================== Variables ============================ //
        public  PlayerButtonController              PlayerButtonPrefab;
        public  Transform                           PlayerButtonsContainer;

        private int                                 m_NameCount;
        private Dictionary
                <string, PlayerButtonController>    m_Buttons;

        // ============================ Methods ============================= //

        public void AddPlayerByType(string type)
        {
            if (type == "Human")
            {
                AddHumanPlayer();
            }
            else if (type == "AI")
            {
                AddAIPlayer();
            }
        }

        public PlayerButtonController AddHumanPlayer()
        {
            return AddPlayer<HumanPlayer>();
        }

        public PlayerButtonController AddAIPlayer()
        {
            return AddPlayer<AIPlayer>();
        }

        private PlayerButtonController AddPlayer<T>() where T: Player, new()
        {
            string                  name;
            T                       player;
            PlayerButtonController  playerButton;
            IUpdatableView<Player>  view;

            player          =   new T();
            name            =   player is HumanPlayer ? GenerateName("Human") : GenerateName("AI");
            player.Name     =   name;
            playerButton    =   Utils.Instantiate<PlayerButtonController>(PlayerButtonPrefab, PlayerButtonsContainer, PlayerButtonsContainer.position, PlayerButtonsContainer.rotation);

            GameController.Instance.Game.Players.AddPlayer(player);

            view = playerButton.CreateView(player);

            view.UpdateView(player);

            playerButton.OnRemove += RemovePlayer;

            m_Buttons.Add(name, playerButton);

            return playerButton;
        }

        private PlayerButtonController ShowPlayer<T>(T player) where T: Player
        {
            PlayerButtonController button;
            IUpdatableView<Player> view;

            button  =   Utils.Instantiate<PlayerButtonController>(PlayerButtonPrefab, PlayerButtonsContainer, PlayerButtonsContainer.position, PlayerButtonsContainer.rotation);
            view    =   button.CreateView(player);

            view.UpdateView(player);

            button.OnRemove += RemovePlayer;

            m_Buttons.Add(player.Name, button);

            return button;
        }

        public void RemovePlayer(string playerName)
        {
            PlayerButtonController button;

            if (m_Buttons.TryGetValue(playerName, out button))
            {
                button.ClearViews();

                button.OnRemove -= RemovePlayer;

                Destroy(button.gameObject);
            }

            m_Buttons.Remove(playerName);

            // Transfer ownership to Neutral player

            Player neutralPlayer;
            Player player;

            neutralPlayer   =   GameController.Instance.Game.Players.Find("Neutral");
            player          =   GameController.Instance.Game.Players.Find(playerName);

            foreach (OwnableMapElement element in GameController.Instance.Game.Map.Grid.GetElements(player))
            {
                if (element.Owner == player)
                {
                    element.Owner = neutralPlayer;
                }
            }

            GameController.Instance.Game.Players.RemovePlayer(playerName);
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
        }

        private string GenerateName(string typeText)
        {
            return typeText + " " + (++m_NameCount);
        }

        // ============================= Unity ============================== //

        protected void OnEnable()
        {
            m_Buttons = new Dictionary<string, PlayerButtonController>();

            if (GameController.Instance != null && GameController.Instance.Game != null)
            {
                foreach (Player player in GameController.Instance.Game.Players)
                {
                    PlayerButtonController button;

                    if (player is AIPlayer)
                    {
                        button = ShowPlayer<AIPlayer>(player as AIPlayer);
                    }
                    else if (player is HumanPlayer)
                    {
                        button = ShowPlayer<HumanPlayer>(player as HumanPlayer);
                    }
                    else
                    {
                        button = null;
                    }

                    if (button != null)
                    {
                        button.OnRemove += RemovePlayer;
                    }
                }    
            }
        }

        protected void OnDisable()
        {
            foreach (KeyValuePair<string, PlayerButtonController> button in m_Buttons)
            {
                button.Value.OnRemove -= RemovePlayer;

                Destroy(button.Value.gameObject);
            }

            m_Buttons.Clear();
        }
    }
}
