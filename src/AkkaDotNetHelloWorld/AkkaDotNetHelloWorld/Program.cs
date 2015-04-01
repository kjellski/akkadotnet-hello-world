using Akka.Actor;
using Akka.Configuration.Hocon;
using AkkaDotNetHelloWorld.Actors;
using AkkaDotNetHelloWorld.Messages;
using System.Configuration;

namespace AkkaDotNetHelloWorld
{
    class Program
    {

        static void Main(string[] args)
        {
            var akkaSection = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            var actorSystem = ActorSystem.Create("dotnethhActorSystem", akkaSection.AkkaConfig);

            var pingActorProps = Props.Create<PingActor>();
            var pingActor = actorSystem.ActorOf(pingActorProps, "pingActor");

            var consoleReaderProps = Props.Create(() => new ConsoleReaderActor(pingActor));
            var consoleReaderActor = actorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");
            consoleReaderActor.Tell(new Start());

            actorSystem.AwaitTermination();
        }
    }
}
