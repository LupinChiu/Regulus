﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Game
{
	public class Hall : Regulus.Utility.IUpdatable
	{
		public event Regulus.Game.OnNewUser NewUserEvent;
        private Regulus.Utility.Updater _Users;

		public Hall()
		{

			_Users = new Regulus.Utility.Updater();
		}
        public void PushUser(Regulus.Game.IUser user)
		{
			user.VerifySuccessEvent += (id) =>
			{
				if (NewUserEvent != null)
					NewUserEvent(id);
				NewUserEvent += user.OnKick;
			};

			user.QuitEvent += () =>
			{
				NewUserEvent -= user.OnKick;
				_Users.Remove(user);
			};
			_Users.Add(user);
		}

		void Regulus.Framework.ILaunched.Launch()
		{

		}

		bool Regulus.Utility.IUpdatable.Update()
		{
			_Users.Update();
			return true;
		}

		void Regulus.Framework.ILaunched.Shutdown()
		{

		}
	}
}