using System;
using System.Collections.Generic;
using System.Linq;
using Battleship.Models;

namespace Battleship
{

    public interface IPlayer
    {
        void ShowBoards();
        void SetShips();
        bool CheckPosition(Position hitPosition);
        bool CommunicateShipDestroy(Position hitPosition);
        void Process(bool isHit, Position hitPosition, bool isDestroyed = false);
    }

    public class Player : IPlayer
    {
        public string Name { get; set; }
        public Boards Boards { get; set; }
        public int DestroyedShips => ships.Count(s => s.isDestroyed());
        public bool DidLost
        {
            get
            {
                return ships.All(s => s.isDestroyed());
            }
        }
        private  IShipsPositionsInitializer _shipsPositionsInitializer { get; set; }
        private  IPlayerShotProvider _playerShotProvider { get; set; }
        private List<Position> hitPositions { get; }
        private List<Ship> ships { get; set; }


        public Player(string name, IPlayerShotProvider playerShotProvider, IShipsPositionsInitializer shipsPositionsInitializer)
        {
            Initialize();
            Name = name;
            hitPositions = new List<Position>();
            Boards = new Boards();
            _shipsPositionsInitializer = shipsPositionsInitializer;
            _playerShotProvider = playerShotProvider;
        }

        public void ShowBoards()
        {
            Boards.Show();
        }

        public void SetShips()
        {
            _shipsPositionsInitializer.InitializeShipsPositions(Boards.Board, ships);
        }

        public Position Shot()
        {
            if (hitPositions.Count > 0)
            {
               Logger.Log($"{Name} search shot");
                var foundPosition = SearchShot();
                if (foundPosition != null)
                {
                    return foundPosition;
                }
            }
            Logger.Log($"{Name} random shot");

            return RandomShot();
        }

        public bool CheckPosition(Position hitPosition)
        {
            var position = Boards.Board.GetPositionFromBoard(hitPosition);
            if (position != null && position.Symbol != null)
            {
                var ship = ships.First(s => s.Short == position.Symbol);
                ship.Hits += 1;
                return true;
            }
            return false;
        }

        public bool CommunicateShipDestroy(Position hitPosition)
        {
            var position = Boards.Board.GetPositionFromBoard(hitPosition);
            return ships.First(s => s.Short == position.Symbol).isDestroyed();
        }

        public void Process(bool isHit, Position hitPosition, bool isDestroyed = false)
        {
            var position = Boards.OpponentBoard.GetPositionFromBoard(hitPosition);
            position.IsAvailable = false;
            if (isDestroyed)
            {
                position.Symbol = "X";
                hitPositions.Clear();
            }
            else if (isHit)
            {
                position.Symbol = "X";
                hitPositions.Add(hitPosition);
            }
            else
            {
                position.Symbol = "M";
            }
        }

        private Position RandomShot()
        {
            return _playerShotProvider.RandomShot(Boards.OpponentBoard);
        }

        private Position SearchShot()
        {
            return _playerShotProvider.SearchShot(Boards.OpponentBoard, hitPositions);
        }
        private void Initialize()
        {
            ships = new List<Ship>();
            ships.Add(new Ship("Carrier", 5, "C"));
            ships.Add(new Ship("Battleship", 4, "B"));
            ships.Add(new Ship("Cruiser", 3, "c"));
            ships.Add(new Ship("Submarine", 3, "S"));
            ships.Add(new Ship("Destroyer", 2, "D"));
        }
    }
}
