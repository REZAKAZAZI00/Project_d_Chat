using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_d_Chat.Core.DTOs.Chat
{
#nullable enable
    public class ChatViewModel
    {
        public string name { get; set; }

        public DateTimeOffset sendAt { get; set; }
        public string text { get; set; }
    }
}
