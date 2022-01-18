using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleship.Models;

namespace Battleship
{
    public interface IPlayerShotProvider
    {
        Position RandomShot(Board board);
        Position SearchShot(Board board, List<Position> hitPositions);
    }

    public class PlayerShotProvider : IPlayerShotProvider
    {
        public Position RandomShot(Board board)
        {
            var random = new Random();
            var availablePositions = board.AvailablePositions;
            var found = false;
            var position = new Position();
            while (!found)
            {
                var randomColumn = random.Next(1, 11);
                var randomRow = random.Next(1, 11);
                var foundPosition = availablePositions.FirstOrDefault(pos => pos.Y == randomRow && pos.X == randomColumn);
                if (foundPosition != null)
                {
                    found = true;
                    position = foundPosition;
                }
            }
            return position;
        }

        public Position SearchShot(Board board, List<Position> hitPositions)
        {
            var availablePositions = board.AvailablePositions;
            Logger.Log($"available positions: {availablePositions.Count}");
            var hitPositionsCount = hitPositions.Count;
            if (hitPositionsCount == 1)
            {
                var firstPosition = hitPositions.First();
                var random = new Random();
                var closePositionsToHit = new List<Position>();
                var positionsInRow = availablePositions.Where(p =>
                    p.X >= firstPosition.X - 1 && p.X <= firstPosition.X + 1 && p.Y == firstPosition.Y).ToList();
                var positionsInColumn = availablePositions
                    .Where(p => p.Y >= firstPosition.Y - 1 && p.Y <= firstPosition.Y + 1 && p.X == firstPosition.X).ToList();
                closePositionsToHit.AddRange(positionsInColumn);
                closePositionsToHit.AddRange(positionsInRow);
                if (closePositionsToHit.Count != 0)
                {
                    var index = random.Next(closePositionsToHit.Count);
                    return closePositionsToHit[index];
                }
                else
                {
                    //in case if algorithm made mistake because of two ships next to each other
                    hitPositions.Clear();
                    var index = random.Next(availablePositions.Count);
                    return availablePositions[index];
                }
            }
            else
            {
                if (hitPositions.First().Y == hitPositions.Last().Y)
                {
                    var sortedHitPositionsByRow = hitPositions.OrderBy(a => a.X);
                    if (availablePositions.Any(p => p.X == sortedHitPositionsByRow.Last().X + 1 && p.Y == sortedHitPositionsByRow.Last().Y))
                    {
                        return new Position(sortedHitPositionsByRow.Last().Y, sortedHitPositionsByRow.Last().X + 1);
                    }
                    else if (availablePositions.Any(p => p.X == sortedHitPositionsByRow.First().X - 1 && p.Y == sortedHitPositionsByRow.First().Y))
                    {
                        return new Position(sortedHitPositionsByRow.First().Y, sortedHitPositionsByRow.First().X - 1);
                    }
                }
                else if (hitPositions.First().X == hitPositions.Last().X)
                {
                    var sortedHitPositionsByColumn = hitPositions.OrderBy(a => a.Y);
            
                    if (availablePositions.Any(p => p.Y == sortedHitPositionsByColumn.Last().Y + 1 && p.X == sortedHitPositionsByColumn.Last().X))
                    {
                        return new Position(sortedHitPositionsByColumn.Last().Y + 1, sortedHitPositionsByColumn.Last().X);
                    }
                    else if (availablePositions.Any(p => p.Y == sortedHitPositionsByColumn.First().Y - 1 && p.X == sortedHitPositionsByColumn.First().X))
                    {
                        return new Position(sortedHitPositionsByColumn.First().Y - 1, sortedHitPositionsByColumn.First().X);
                    }
                }
            }
            return null;
        }
    }
}
