using Akka.Actor;
using AkkaDotNetHelloWorld.Messages;
using System;
using System.Collections.Generic;

namespace AkkaDotNetHelloWorld.Actors
{
    class PingActor : ReceiveActor
    {
        public List<ActorRef> PongActors { get; private set; }

        public PingActor()
        {
            PongActors = new List<ActorRef>();
            Receive<string>(s => SenderContinueAfter(() =>
                HandleString(s)));
            Receive<PingRequest>(p => SenderContinueAfter(() =>
                HandlePongRequest(p)));
            Receive<PongResponse>(response => HandleResponse(response));
        }

        private void HandleResponse(PongResponse response)
        {
            var now = DateTime.UtcNow;
            var delta = now - response.CreatedAt;
            Console.WriteLine("Pong->Ping: " + delta.Milliseconds + "ms - " + response.Number);
        }

        private void SenderContinueAfter(Action action)
        {
            action();
            Sender.Tell(new ReadConsole());
        }

        private void HandlePongRequest(PingRequest pingRequest)
        {
            PongActors.Clear();
            Console.WriteLine("PingRequest: " + pingRequest.CreatedAt + " - " + pingRequest.Number);
            Console.WriteLine("Creating this many PongActors: ");
            for (var i = 0; i < pingRequest.Number; i++)
            {
                var pongActorRef = Context.ActorOf<PongActor>();
                PongActors.Add(pongActorRef);
                pongActorRef.Tell(pingRequest);
            }
        }

        private void HandleString(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return;

            Console.WriteLine("String: " + s);
        }
    }
}