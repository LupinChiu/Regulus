using System;

namespace Regulus.Project.SamebestKeys
{
    using Regulus.Project.SamebestKeys.Serializable;
    using Regulus.Remoting;

	/// <summary>
	/// 登入驗證
	/// </summary>
    public interface IVerify
    {
        
        Value<bool> CreateAccount(string name, string password);
        Value<LoginResult> Login(string name, string password);
        void Quit();        
    };

    public interface IParking
    {
        Value<bool> CheckActorName(string name );
        Value<bool> CreateActor(EntityLookInfomation cai);
        Value<EntityLookInfomation[]> DestroyActor(string name);
        Value<EntityLookInfomation[]> QueryActors();
        void Back();
        Value<string> Select(string name);
    }

	public interface IMapInfomation
	{
        Value<Types.Polygon[]> QueryWalls();
	}

	/// <summary>
	/// 玩家自己
	/// </summary>
    public interface IPlayer
    {
        Guid Id { get; }
        string Name { get; }
        float Speed { get; }
        float Direction { get; }		
        
        
        void Logout();
        void ExitWorld();        
        void SetPosition(float x,float y);		
        void SetVision(int vision);

        void SetSpeed(float speed);
        void ChangeMode();
        void Cast(int skill);
        void Walk(float direction);
        void Stop(float direction);
        void Say(string message);
		
        void BodyMovements(ActionStatue action_statue);
        Value<string> QueryMap();        
        void Goto(string map , float x , float y);
        
    }

	/// <summary>
	/// 觀察功能
	/// </summary>
    public interface IObservedAbility 
    {
        ActorMode Mode { get; }
        string Name { get; }
        Guid Id { get; }
        Regulus.Types.Vector2 Position { get; }
        int Shell { get; }
        float Direction { get; }        
        event Action<MoveInfomation> ShowActionEvent;
        event Action<string> SayEvent;
        
    }

	/// <summary>
	/// 移動功能
	/// </summary>
    public interface IMoverAbility
    {
        Regulus.Types.Polygon Polygon { get; }

        void Act(ActionCommand action_command);

        void Update(long time, System.Collections.Generic.IEnumerable<Regulus.Types.Polygon> obbs);
    }

    public interface ITraversable
    {
        Value<CrossStatus> GetStatus();
        void Ready();
    }

    
}


