using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class StartupArgs
    {
        [Option(longName: "APIserver", Required = false)]
        public string ServerAddres { get; set; }
    }
}
