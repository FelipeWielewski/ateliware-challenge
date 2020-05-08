using System;
using System.Collections.Generic;
using System.Text;

namespace Hiring.Challenge.Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
    }
}
