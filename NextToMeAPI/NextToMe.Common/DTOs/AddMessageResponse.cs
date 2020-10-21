using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NextToMe.Common.DTOs
{
    public class AddMessageResponse : AddMessageRequest
    {
        public DateTime CreatedAt { get; set; }
        
        public string From { get; set; }
    }
}
