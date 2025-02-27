using NiceStore.Core.Messages;

namespace NiceStore.Core.Bus
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T eventt) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command; 
    }
}
