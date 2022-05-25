using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Telegram.States
{
    public enum UserState
    {
        ProcessNotStarted,
        SelectingFromCurrency,
        SelectingToCurrency,
    }
}
