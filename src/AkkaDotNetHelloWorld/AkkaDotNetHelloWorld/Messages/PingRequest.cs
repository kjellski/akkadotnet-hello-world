using System;

namespace AkkaDotNetHelloWorld.Messages
{
    class PingRequest
    {
        public PingRequest(int number, DateTime createdAt)
        {
            Number = number;
            CreatedAt = createdAt;
        }

        public int Number { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}