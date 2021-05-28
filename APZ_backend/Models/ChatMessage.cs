using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class ChatMessage
    {
        public string ChatToken { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
