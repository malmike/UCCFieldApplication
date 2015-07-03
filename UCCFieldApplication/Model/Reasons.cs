using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCCFieldApplication.Model
{

    public class Reasons
    {
        public string Reason { get; set; }
    }

    public class ReasonRootObject
    {
        public List<Reasons> data { get; set; }
    }

}