using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using CloudOnProject.Models;
using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace CloudOnProject.Data
{
    public static class DbInitialize
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Product.Any())
            {
                return;
            }

            string url = "https://cloudonapi.oncloud.gr/s1services/JS/updateItems/cloudOnTest";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            HttpWebResponse res = (HttpWebResponse)request.GetResponse();

            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.Default);
            string json = sr.ReadToEnd();
            sr.Close();

            
            string str1 = json.Remove(0,24);
            string str2 = str1.Remove(str1.Length - 1, 1);
            
            
            var res2 = JsonConvert.DeserializeObject<Product[]>(str2);
            

            foreach (var product in res2)
            {
                context.Add(product);
                context.SaveChanges();
            }


        }

        
    }
}
