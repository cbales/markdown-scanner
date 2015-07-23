﻿namespace ApiDocs.Validation
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Page annotation allows you to make page-level annotations for a variety of reasons
    /// </summary>
    public class PageAnnotation
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description"), MaxLength(156)]
        public string Description { get; set; }

        [JsonProperty("keywords"), MaxLength(156)]
        public string Keywords { get; set; }

        [JsonProperty("cononicalUrl")]
        public string CononicalUrl { get; set; }

        [JsonProperty("section")]
        public string Section { get; set; }

        [JsonProperty("tocPath")]
        public string TocPath { get; set; }

        [JsonProperty("headerAdditions")]
        public string[] HeaderAdditions { get; set; }

        [JsonProperty("footerAdditions")]
        public string[] FooterAdditions { get; set; }
    }
    

    public class MaxLengthAttribute : Attribute 
    {
        public MaxLengthAttribute(int maximumLength)
        {
            this.MaximumLength = maximumLength;
        }

        public int MaximumLength { get; set; }

        public static int GetMaxLength(Type type, string propertyName)
        {
            var attribute = (MaxLengthAttribute)type.GetProperty(propertyName).GetCustomAttributes(true).FirstOrDefault(x => x is MaxLengthAttribute);
            if (null != attribute)
            {
                return attribute.MaximumLength;
            }
            return -1;
        }
    }
}