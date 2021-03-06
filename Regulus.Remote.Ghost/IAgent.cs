using Regulus.Network;
using System;

namespace Regulus.Remote.Ghost
{
    public interface IAgent : INotifierQueryable
    {

        /// <summary>
        ///     Active
        /// </summary>
        bool Active { get; }
        /// <summary>
        ///     Ping
        /// </summary>
        long Ping { get; }


        /// <summary>
        /// 錯誤的方法呼叫
        /// 如果呼叫的方法參數有誤則會回傳此訊息.
        /// 事件參數:
        ///     1.方法名稱
        ///     2.錯誤訊息
        /// 會發生此訊息通常是因為client與server版本不相容所致.
        /// </summary>
        event Action<string, string> ErrorMethodEvent;


       


        void Start(IStreamable stream);
        void Stop();

        void Update();
    }
}