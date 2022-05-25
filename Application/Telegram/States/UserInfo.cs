using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Telegram.States
{
    public class UserInfo
    {
        public UserState CurrentState { get; set; } = UserState.ProcessNotStarted;
        public int UserId { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
    }
}
