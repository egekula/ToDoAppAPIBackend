using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Core.Entities.Concrate
{
    public class UserOperationClaim : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid OperationClaimId { get; set; }



    }
}
