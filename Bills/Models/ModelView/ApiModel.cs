﻿namespace Bills.Models.ModelView
{
    public class ApiModel 
    {
        public ApiModel()
        {
      

        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
