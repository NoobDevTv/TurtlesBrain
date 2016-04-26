using System;

namespace TurtlesBrain.Shared
{
    public interface ITurtleApiMessage
    {

    }

    public abstract class TurtleApiMessage<T> : ITurtleApiMessage
        where T : TurtleApiMessage<T>, new()
    {
        public static int MessageType;
    }

    public class CountRequest : TurtleApiMessage<CountRequest> { }

    public class CountResponse : TurtleApiMessage<CountResponse>
    {
        public int Result { get; set; }
    }

    public class TurtleMessage : TurtleApiMessage<TurtleMessage>
    {
        public string Label { get; set; }
    }

    public class Response : TurtleApiMessage<Response>
    {
        public string Label { get; set; }
        public string Content { get; set; }
    }

    public class ClientMessage : TurtleApiMessage<ClientMessage>
    {
        public string Label { get; set; }
        public string Command { get; set; }
    }

    public class PingMessage : TurtleApiMessage<PingMessage>
    {
        public int Bbq { get; set; }
        public string Bar { get; set; }
    }
    public class PongMessage : TurtleApiMessage<PongMessage> { }
}
