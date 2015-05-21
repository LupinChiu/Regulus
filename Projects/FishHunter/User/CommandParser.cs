﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Regulus;

namespace VGame.Project.FishHunter
{
    using Regulus.Extension;
    public class CommandParser : Regulus.Framework.ICommandParsable<IUser>
    {
    
        
        private Regulus.Utility.Command _Command;
        private Regulus.Utility.Console.IViewer _View;
        private IUser _User;

        public CommandParser(Regulus.Utility.Command command, Regulus.Utility.Console.IViewer view, IUser user)
        {            
            this._Command = command;
            this._View = view;
            this._User = user;            
        }
        void Regulus.Framework.ICommandParsable<IUser>.Clear()
        {
            _DestroySystem();   
        }

        private void _DestroySystem()
        {
            _Command.Unregister("Agent");
        }
        
        private void _ConnectResult(bool result)
        {
            _View.WriteLine(string.Format("Connect result {0}", result));

        }

        void Regulus.Framework.ICommandParsable<IUser>.Setup(Regulus.Remoting.IGPIBinderFactory factory)
        {
            _CreateSystem();

            _CreateConnect(factory);


            _CreateOnline(factory);


            _CreateVerify(factory);

            _CreatePlayer(factory);
        }

        private void _CreateSystem()
        {
            _Command.Register("Agent", _ShowIOHandler);
        }

        private void _ShowIOHandler()
        {

            _View.WriteLine(string.Format("Agent Fps:{0} Power:{1:0.00%}", Regulus.Remoting.Ghost.Native.Agent.Fps, Regulus.Remoting.Ghost.Native.Agent.Power));
        }

        private void _CreatePlayer(Regulus.Remoting.IGPIBinderFactory factory)
        {
            var player = factory.Create<VGame.Project.FishHunter.IPlayer>(_User.PlayerProvider);

            player.Bind("Hit[bulletid,fishid]", (gpi) => { return new Regulus.Remoting.CommandParamBuilder().Build<int, int>((b,f) => { gpi.Hit(b , new int[] {f}); }); });
            player.Bind("RequestBullet", (gpi) => { return new Regulus.Remoting.CommandParamBuilder().BuildRemoting<int>(gpi.RequestBullet, _GetBullet ); });

            player.SupplyEvent += _RegisgetPlayerEvent;
        }

        private void _GetBullet(int obj)
        {
            _View.WriteLine("get bullet id" + obj.ToString());
        }

        private void _RegisgetPlayerEvent(IPlayer source)
        {
            source.MoneyEvent += (money) => { _View.WriteLine("player money " + money.ToString()); };
            source.DeathFishEvent += (fish) => { _View.WriteLine(string.Format("fish{0} is dead", fish)); };
        }

        private void _CreateVerify(Regulus.Remoting.IGPIBinderFactory factory)
        {
            var verify = factory.Create<VGame.Project.FishHunter.IVerify>(_User.VerifyProvider);
            verify.Bind("Login[result,id,password]", (gpi) => { return new Regulus.Remoting.CommandParamBuilder().BuildRemoting<string, string, bool>(gpi.Login, _VerifyResult); });
        }

        private void _CreateOnline(Regulus.Remoting.IGPIBinderFactory factory)
        {
            var online = factory.Create<Regulus.Utility.IOnline>(_User.Remoting.OnlineProvider);
            online.Bind("Disconnect", (gpi) => { return new Regulus.Remoting.CommandParamBuilder().Build(gpi.Disconnect); });
            online.Bind("Ping", (gpi) => { return new Regulus.Remoting.CommandParamBuilder().Build(() => { _View.WriteLine("Ping : " + gpi.Ping.ToString()); }); });
        }

        private void _CreateConnect(Regulus.Remoting.IGPIBinderFactory factory)
        {
            var connect = factory.Create<Regulus.Utility.IConnect>(_User.Remoting.ConnectProvider);
            connect.Bind("Connect[result , ipaddr ,port]", (gpi) => { return new Regulus.Remoting.CommandParamBuilder().BuildRemoting<string, int, bool>(gpi.Connect, _ConnectResult); });
        }

        private void _VerifyResult(bool result)
        {
            _View.WriteLine(string.Format("Verify result {0}", result));
        }

        
    }
}
