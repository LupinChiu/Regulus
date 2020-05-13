﻿using Regulus.Utility;
using System;
using System.Linq;
using System.Reflection;

namespace Regulus.Remote.Client
{
    public class AgentCommandRegister 
    {
        readonly System.Collections.Generic.List<AgentCommand> _Invokers;
        
        private readonly Command _Command;
        private readonly AgentCommandVersionProvider _VersionProvider;

        public AgentCommandRegister(Command command)
        {
            _VersionProvider = new AgentCommandVersionProvider();
            _Invokers = new System.Collections.Generic.List<AgentCommand>();
            this._Command = command;
        }

          
        public void Regist(Type type, object instance)
        {
            
            foreach (var method in type.GetMethods())
            {                
                var invoker = new MethodStringInvoker(instance, method);
                var ac = new AgentCommand(_VersionProvider, type , invoker);
                _Invokers.Add(ac);            
                _Command.Register(ac.Name, (args) => invoker.Invoke(args) , method.ReturnParameter.ParameterType , method.GetParameters().Select( (p)=> p.ParameterType).ToArray()  );
            }
            
        }

        public void Unregist(object instance)
        {
            var removes = _Invokers.Where(i => i.Target == instance).ToArray(); 
            foreach(var invoker in removes)
            {
                _Command.Unregister(invoker.Name);
                _Invokers.Remove(invoker);
            }            
        }
    }
}