﻿using EntityFrameworkCoreApp.DataStorage.Models;
using System;

namespace EntityFrameworkCoreApp.BusinessLogic.Services
{
    public class Email
    {
        public Guid EmailId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string SendTo { get; set; }

        public bool IsSent { get; set; }

        public DateTime GenerateDateTimeUTC { get; set; }

        public DateTime? SendDateTimeUTC { get; set; }
    }

    public partial class BusinessLogicMappingProfile
    {
        private void InitializeEmail()
        {
            CreateMap<EmailEntity, Email>();
        }
    }
}
