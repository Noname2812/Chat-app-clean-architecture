using System.Reflection;

namespace ChatApp.Infrastructure.Dapper
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }

}
