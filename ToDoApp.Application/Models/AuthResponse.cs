using System;
using System.Collections.Generic;

namespace ToDoApp.Application.Models
{
    public class AuthResponse
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
