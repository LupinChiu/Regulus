using Regulus.Network;
using System;

namespace Regulus.Remote.Ghost
{

    /// <summary>
    ///     �N�z��
    /// </summary>
    /// 

    public interface IAgent : INotifierQueryable
    {


        /// <summary>
        ///     Ping
        /// </summary>
        long Ping { get; }


        /// <summary>
        /// ���~����k�I�s
        /// �p�G�I�s����k�ѼƦ��~�h�|�^�Ǧ��T��.
        /// �ƥ�Ѽ�:
        ///     1.��k�W��
        ///     2.���~�T��
        /// �|�o�ͦ��T���q�`�O�]��client�Pserver�������ۮe�ҭP.
        /// </summary>
        event Action<string, string> ErrorMethodEvent;


        /// <summary>
        /// ���ҿ��~
        /// �N��P���A���ݪ����ҽX����
        /// �ƥ�Ѽ�:
        ///     1.���A�����ҽX
        ///     2.���a���ҽX
        /// �|�o�ͦ��T���q�`�O�]��client�Pserver�������ۮe�ҭP.
        /// </summary>
        event Action<byte[], byte[]> ErrorVerifyEvent;


        void Start(IStreamable peer);
        void Stop();

        void Update();
    }
}