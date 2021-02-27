using System;

namespace CloudProvider.SDK.Persistence.Domain
{
    public class BaseModel
    {
        public string Name { get; set; }
        
        public object Data { get; set; }
    }
}
