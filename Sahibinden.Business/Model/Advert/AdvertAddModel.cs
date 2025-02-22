﻿using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Sahibinden.Entities.Concrete;
using Sahibinden.Model.AdvertDetail;
using Sahibinden.Model.Category;
using Sahibinden.Model.Image;
using System.Text.Json.Serialization;

namespace Sahibinden.Business.Model.Advert
{
    public class AdvertAddModel
    {
        [JsonProperty("Name")]
        
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public  DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public int ImageId { get; set; }
      
    }
}
