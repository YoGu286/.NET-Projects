using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudOnProject.Models
{
    public class Product
    {

        [Key]
        [JsonProperty("externalId")]
        [RegularExpression(@"^[0-9]*$")]
        public string ExternalId { get; set; }

        [Required]
        [JsonProperty("code")]
        [RegularExpression(@"^[A-Z][0-9]*$")]
        public string Code { get; set; }

        [Required]
        [JsonProperty("description")]
        public string Description { get; set; }


        [JsonProperty("name")]
        public string? Name { get; set; }

        [Required]
        [JsonProperty("barcode")]
        [RegularExpression(@"^[0-9]*$")]
        public string Barcode { get; set; }

        [DisplayName("Retail Price")]
        [Required]
        [JsonProperty("retailPrice")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$")]
        public string RetailPrice { get; set; }

        [DisplayName("Wholesale Price")]
        [Required]
        [JsonProperty("wholesalePrice")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$")]
        public string WholesalePrice { get; set; }

        [Required]
        [JsonProperty("discount")]
        [RegularExpression(@"^[0-9]*$")]
        public string Discount { get; set; }
    }
}
