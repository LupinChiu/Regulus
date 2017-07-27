﻿using System;

namespace Regulus.Network.RUDP
{
    public interface ISendable
    {
        void Transport(SocketMessage message);

        event Action<SocketMessage> DoneEvent;
    }
}