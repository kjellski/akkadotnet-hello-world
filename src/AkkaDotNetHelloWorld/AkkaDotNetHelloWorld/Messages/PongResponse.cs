using System;

namespace AkkaDotNetHelloWorld.Messages
{
    class PongResponse
    {
        public PongResponse(int number, DateTime createdAt)
        {
            CreatedAt = createdAt;
            Number = number;
        }

        public int Number { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}