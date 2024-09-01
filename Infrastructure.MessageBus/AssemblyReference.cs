
using System.Reflection;


namespace Infrastructure.MessageBus
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
