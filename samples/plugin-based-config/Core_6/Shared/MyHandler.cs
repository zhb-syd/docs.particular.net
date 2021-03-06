using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

public class MyHandler :
    IHandleMessages<MyMessage>
{
    static ILog log = LogManager.GetLogger<MyHandler>();
    public Task Handle(MyMessage message, IMessageHandlerContext context)
    {
        log.Info("Handled Message");
        return Task.FromResult(0);
    }
}