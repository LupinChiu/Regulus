﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;


using Regulus.Framework;

namespace Regulus.Remoting.Soul.Native
{
	internal class Peer : IRequestQueue, IResponseQueue, IBootable
	{
		public delegate void DisconnectCallback();

		private event Action _BreakEvent;

		private event Action<Guid, string, Guid, byte[][]> _InvokeMethodEvent;

		public event DisconnectCallback DisconnectEvent;

		private class Request
		{
			public Guid EntityId { get; set; }

			public string MethodName { get; set; }

			public Guid ReturnId { get; set; }

			public byte[][] MethodParams { get; set; }
		}

		private static readonly object _LockRequest = new object();

		private static readonly object _LockResponse = new object();

		private readonly PackageReader<RequestPackage> _Reader;

		private readonly Regulus.Collection.Queue<RequestPackage> _Requests;

		private readonly Regulus.Collection.Queue<ResponsePackage> _Responses;

		private readonly Socket _Socket;

		private readonly SoulProvider _SoulProvider;

		private readonly PackageWriter<ResponsePackage> _Writer;


		private volatile bool _EnableValue;
		private bool _Enable
		{
			get
			{
				lock (_EnableLock)
				{
					return _EnableValue;
				}
			}

			set
			{
				lock (_EnableLock)
				{
					_EnableValue = value;
				}
			}

		}
		

		private object _EnableLock;

		public static bool IsIdle
		{
			get { return Peer.TotalRequest <= 0 && Peer.TotalResponse <= 0; }
		}

		public static int TotalRequest { get; private set; }

		public static int TotalResponse { get; private set; }

		public ISoulBinder Binder
		{
			get { return _SoulProvider; }
		}

		public CoreThreadRequestHandler Handler
		{
			get { return new CoreThreadRequestHandler(this); }
		}

		public Peer(Socket client)
		{
			_EnableLock = new object();

			_Socket = client;
			_SoulProvider = new SoulProvider(this, this);
			_Responses = new Regulus.Collection.Queue<ResponsePackage>();
			_Requests = new Regulus.Collection.Queue<RequestPackage>();

			_Enable = true;

			_Reader = new PackageReader<RequestPackage>();
			_Writer = new PackageWriter<ResponsePackage>();
		}

		void IBootable.Launch()
		{
			_Reader.DoneEvent += _RequestPush;
			_Reader.ErrorEvent += () => { _Enable = false; };
			_Reader.Start(_Socket);

			_Writer.ErrorEvent += () => { _Enable = false; };
			_Writer.CheckSourceEvent += _ResponsePop;
			_Writer.Start(_Socket);
		}

		void IBootable.Shutdown()
		{
			try
			{
				_Socket.Shutdown(SocketShutdown.Both);
				_Socket.Close();
			}
			catch (System.Net.Sockets.SocketException se)
			{
				Regulus.Utility.Log.Instance.WriteInfo(string.Format("Socket shutdown socket exception.{0}" , se.Message));
			}
			catch (Exception e)
			{
				Regulus.Utility.Log.Instance.WriteInfo(string.Format("Socket shutdown exception.{0}", e.Message));
			}
			
			_Reader.DoneEvent -= _RequestPush;
			_Reader.Stop();
			_Writer.CheckSourceEvent -= _ResponsePop;
			_Writer.Stop();

			lock(Peer._LockResponse)
			{
				var pkgs = _Responses.DequeueAll();
				Peer.TotalResponse -= pkgs.Length;
			}

			lock(Peer._LockRequest)
			{
				var pkgs = _Requests.DequeueAll();
				Peer.TotalRequest -= pkgs.Length;
			}
		}

		event Action<Guid, string, Guid, byte[][]> IRequestQueue.InvokeMethodEvent
		{
			add { _InvokeMethodEvent += value; }
			remove { _InvokeMethodEvent -= value; }
		}

		event Action IRequestQueue.BreakEvent
		{
			add { _BreakEvent += value; }
			remove { _BreakEvent -= value; }
		}

		void IRequestQueue.Update()
		{
			if(_Connected() == false)
			{
				Disconnect();
				DisconnectEvent();
				return;
			}

			_SoulProvider.Update();
			RequestPackage[] pkgs = null;
			lock(Peer._LockRequest)
			{
				pkgs = _Requests.DequeueAll();
				Peer.TotalRequest -= pkgs.Length;
			}

			foreach(var pkg in pkgs)
			{
				var request = _TryGetRequest(pkg);

				if(request != null)
				{
					if(_InvokeMethodEvent != null)
					{
						_InvokeMethodEvent(request.EntityId, request.MethodName, request.ReturnId, request.MethodParams);
					}
				}
			}
		}

		void IResponseQueue.Push(ServerToClientOpCode cmd, byte[] data)
		{
			
			lock(Peer._LockResponse)
			{
				
				if (_Enable)
				{
					Peer.TotalResponse++;
					_Responses.Enqueue(
						new ResponsePackage
                        {
							Code = cmd,
							Data = data
                        });
				}                
			}
		}

		private void _RequestPush(RequestPackage package)
		{
			lock(Peer._LockRequest)
			{
				_Requests.Enqueue(package);
				Peer.TotalRequest++;
			}
		}

		private Request _TryGetRequest(RequestPackage package)
		{
			if(package.Code == ClientToServerOpCode.Ping)
			{
				(this as IResponseQueue).Push(ServerToClientOpCode.Ping, new byte[0]);
				return null;
			}

			if(package.Code == ClientToServerOpCode.CallMethod)
			{
				/*var EntityId = new Guid(package.Args[0]);
				var MethodName = Encoding.Default.GetString(package.Args[1]);

				byte[] par = null;
				var ReturnId = Guid.Empty;
				if(package.Args.TryGetValue(2, out par))
				{
					ReturnId = new Guid(par);
				}

				var MethodParams = (from p in package.Args
									where p.Key >= 3
									orderby p.Key
									select p.Value).ToArray();*/


			    var data = package.Data.ToPackageData<PackageCallMethod>();                
                return _ToRequest(data.EntityId, data.MethodName, data.ReturnId, data.MethodParams);
			}

			if(package.Code == ClientToServerOpCode.Release)
			{
				//var EntityId = new Guid(package.Args[0]);


                var data = package.Data.ToPackageData<PackageRelease>();
                _SoulProvider.Unbind(data.EntityId);
				return null;
			}

			return null;
		}

		private Request _ToRequest(Guid entity_id, string method_name, Guid return_id, byte[][] method_params)
		{
			return new Request
			{
				EntityId = entity_id, 
				MethodName = method_name, 
				MethodParams = method_params, 
				ReturnId = return_id
			};
		}

		private bool _Connected()
		{
			return _Enable && _Socket.Connected;
		}

		internal void Disconnect()
		{
			if(_BreakEvent != null)
			{
				_BreakEvent();
			}
		}

		private ResponsePackage[] _ResponsePop()
		{
			lock(Peer._LockResponse)
			{
				var pkgs = _Responses.DequeueAll();
				Peer.TotalResponse -= pkgs.Length;
				return pkgs;
			}
		}
	}
}
