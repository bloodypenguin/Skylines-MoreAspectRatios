using System;

namespace MoreAspectRatios.Redirection
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class TargetTypeAttribute : Attribute
    {
        public TargetTypeAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}