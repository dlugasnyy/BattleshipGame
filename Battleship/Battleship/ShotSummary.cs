using System;
using System.Collections.Generic;
using System.Text;
using Battleship.Models;

namespace Battleship
{
    public enum ShotResult
    {
        Hit,
        Destroyed,
        Missed
    }

    public class ShotSummary
    {
        public ShotResult Result { get; private set; }
        public Position Position { get; private set; }
    }
}
