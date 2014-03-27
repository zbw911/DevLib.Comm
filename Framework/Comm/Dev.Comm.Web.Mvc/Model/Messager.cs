using System.Collections.Generic;

namespace Dev.Comm.Web.Mvc.Model
{
    public class Messager
    {
        public List<string> MessageList { get; set; }

        public string redirectto { get; set; }

        public string to_title { get; set; }

        public int time { get; set; }

        public string return_msg { get; set; }

        public string tips { get; set; }
    }
}
