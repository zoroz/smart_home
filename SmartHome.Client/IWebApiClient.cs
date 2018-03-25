using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHome.Client
{
    public interface IWebApiClient
    {
        SOnOff SOnOff();

        Seltron Seltron();
    }
}
