using System;
using System.Collections.Generic;

namespace CloudProvider.SDK.Persistence.Domain
{
    public class CloudInfrastructure : BaseModel
    {
        public List<Resource> Resources { get; set; }
    }
}
