using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Telegram.States
{
    public static class UserStateStorage
    {
        public static ConcurrentDictionary<long, UserInfo> Users = new();
    }
}
