using System;

namespace Regulus.Project.SamebestKeys
{
    using Regulus.Project.SamebestKeys.Serializable;
    using Regulus.Remoting;

	/// <summary>
	/// �n�J����
	/// </summary>
    public interface IVerify
    {
        
        Value<bool> CreateAccount(string name, string password);
        Value<LoginResult> Login(string name, string password );
        void Quit();        
    };

    public interface IParking
    {
        Value<bool> CheckActorName(string name );
        Value<bool> CreateActor(EntityLookInfomation cai , EntityPropertyInfomation.IDENTITY identity);
        Value<EntityLookInfomation[]> DestroyActor(string name);
        Value<EntityLookInfomation[]> QueryActors();
        void Back();
        Value<string> Select(string name);
    }


    public interface ILevelSelector
    {
        Remoting.Value<string[]> QueryLevels();
        Remoting.Value<bool> Select(string id);
        void Back();
    }


	public interface IMapInfomation
	{
        Value<CustomType.Polygon[]> QueryWalls();
	}
    public interface IBelongings
    {
        event Action<int> CoinsEvent;
        event Action<int> CashEvent;
        Remoting.Value<int> QueryCoins();
        Remoting.Value<int> QueryCash();
        Remoting.Value<int> AddCoins(int coins);
    }
	/// <summary>
	/// ���a�ۤv
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

        void SetEnergy(int energy);

        void SetSpeed(float speed);
        void ChangeMode();
        void Cast(int skill);
        void Walk(float direction, long time);
        void Stop(float direction);
        void Say(string message);

        void Speak(float[] voice_stream);
		
        void BodyMovements(ActionStatue action_statue);
        
        void Goto(string map , float x , float y);

        float Energy { get; }
        float MaxEnergy { get; }

        
    }
    
	/// <summary>
	/// �[��\��
	/// </summary>
    public interface IObservedAbility 
    {     
        ActorMode Mode { get; }
        string Name { get; }
        Guid Id { get; }
        Regulus.CustomType.Vector2 Position { get; }
        int Shell { get; }
        float Direction { get; }        
        event Action<MoveInfomation> ShowActionEvent;
        event Action<string> SayEvent;
        event Action<float[]> SpeakEvent;
        
    }



	/// <summary>
	/// ���ʥ\��
	/// </summary>
    public interface IMoverAbility
    {
        Regulus.CustomType.Polygon Polygon { get; }

        void Act(ActionCommand action_command);

        void Update(long time, System.Collections.Generic.IEnumerable<Regulus.CustomType.Polygon> obbs);
    }

    public interface ITraversable
    {
        Value<CrossStatus> GetStatus();
        void Ready();
    }


    public interface IIdle
    {
        void GotoRealm(string realm);
    }

    public interface ISessionStuff
    {
        Guid Id { get; }
        event Action<float[]> SpeakEvent;
        void Speak(float[] speak);
    }

    public interface ISessionRequester
    {
        Remoting.Value<bool> Requester(string target , int coin);
    }

    public interface ISessionResponse
    {
        void Yes();
        void No();
    }
    
    public interface ISessionTeacher
    {

        string Lession { get; }
        event Action<long> CurrentTimeEvent;
        
        void Next(int step);
        void SetScore(SessionScoreType type,int score);
        void SetTexture(string name);
        void Done();
    }

    public interface ISessionStudent
    {
        string Lession { get; }
        event Action<long> CurrentTimeEvent;
        event Action<int> NextEvent;
        event Action<string> TextureEvent;

        event Action<SessionScore[]> DoneEvent;        
    }

    

}


