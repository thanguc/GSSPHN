using EmptyWeb.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyWeb.Models
{
    public class HtmlTemplate
    {
        public Guid HtmlTemplateId { get; set; } = Guid.NewGuid();
        public string TemplateCode { get; set; }
        [AllowHtml]
        public string Content { get; set; }
    }
}