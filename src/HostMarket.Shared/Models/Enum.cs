using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Shared.Models; 

public enum ServerStatus
{
    Available,
    Purchased
}

public enum Status
{
    Active, 
    Deleted
}
public enum UserRole
{
    Admin,
    User,
    ServerManager
}
public enum TransactionStatus
{
    Finished,
    Error,
    Pending,
    Denied
}

