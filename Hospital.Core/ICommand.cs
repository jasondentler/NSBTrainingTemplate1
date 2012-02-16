using System;

namespace Hospital
{
    public interface ICommand : IMessage
    {

        Guid CommandId { get; }

    }
}
