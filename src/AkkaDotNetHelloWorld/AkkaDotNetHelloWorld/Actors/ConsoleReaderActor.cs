using Akka.Actor;
using AkkaDotNetHelloWorld.Messages;
using System;

namespace AkkaDotNetHelloWorld.Actors
{
    internal class ConsoleReaderActor : ReceiveActor
    {
        private readonly ActorRef _pingActor;
        public const string ExitCommand = "exit";

        public ConsoleReaderActor(ActorRef pingActor)
        {
            _pingActor = pingActor;
            Receive<ReadConsole>(_ => ReadConsole());
            Receive<Start>(_ =>
            {
                PrintInstructions();
                Self.Tell(new ReadConsole());
            });
        }

        private void PrintInstructions()
        {
            Console.WriteLine("\n    Enter a number to create that much Pong Actors.   \n");
        }

        private void ReadConsole()
        {
            Console.Write("user> ");
            var read = Console.ReadLine();
            if (!string.IsNullOrEmpty(read) && String.Equals(read, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                // if user typed ExitCommand, shut down the entire actor system (allows the process to exit)
                Context.System.Shutdown();
                return;
            }

            int number;
            if (int.TryParse(read, out number) && number > 0)
            {
                _pingActor.Tell(new PingRequest(number, DateTime.UtcNow));
            }
            else
            {
                _pingActor.Tell(read);
            }
        }
    }
}
