﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Regulus.Lockstep
{
    public class Driver<TCommand>
    {
        private readonly long _Interval;
        private List<Player<TCommand>> _Players;
        private readonly History<Record> _History;
        public Driver(long interval)
        {
            _Interval = interval;
            _History = new History<Record>();
            _Players = new List<Player<TCommand>>();
        }
        public IPlayer<TCommand> Regist(ICommandProvidable<TCommand> provider)
        {
            var player = new Player<TCommand>(provider , _History.GetEnumerable());
            _Players.Add(player);
            return player;
        }

        public class Record
        {   
            public Guid Id;
            public TCommand Command;

        }

        public void Advance(long delta)
        {
            var step = _History.Write(from p in _Players select new Record() {Id = p.Id, Command = p.Providable.Current});
            foreach (var player in _Players)
            {
                player.Push(step);
            }
        }

        public bool Unregist(IPlayer<TCommand> player)
        {
            return _Players.RemoveAll( p => p.Id ==  player.Id) > 0;
        }
    }
}