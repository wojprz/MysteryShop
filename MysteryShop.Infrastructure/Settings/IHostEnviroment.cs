using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryShop.Infrastructure.Settings
{
    public interface IHostEnviroment
    {
        string RootPath { get; set; }
    }

}
