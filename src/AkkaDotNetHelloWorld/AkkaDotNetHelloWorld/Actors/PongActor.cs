using Akka.Actor;
using AkkaDotNetHelloWorld.Messages;
using System;

namespace AkkaDotNetHelloWorld.Actors
{
    internal class PongActor : ReceiveActor
    {
        public PongActor()
        {
            Receive<PingRequest>(req => HandleRequest(req));
            Receive<object>(_ => Unhandled(_));
        }

        private void HandleRequest(PingRequest req)
        {
            var now = DateTime.UtcNow;
            var delta = now - req.CreatedAt;
            Console.WriteLine("Ping->Pong: " + delta.Milliseconds + "ms - " + req.Number);
            Sender.Tell(new PongResponse(req.Number, now), Self);
        }
    }
}