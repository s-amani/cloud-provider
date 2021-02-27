using System;

namespace CloudProvider.SDK.Persistence.Domain
{
    public class Resource : BaseModel
    {
        public string Path { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
