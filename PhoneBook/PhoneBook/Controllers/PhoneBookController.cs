using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.DataAccess;
using PhoneBook.Models;
using System.IO;

namespace PhoneBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        static string binaryFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Records.dat");

        [HttpPost("AddNumber")]
        public Record AddNumber([FromBody] Record phoneBook)
        {
            using (BinaryWriter writer = new BinaryWriter(new FileStream(binaryFile, FileMode.Append)))
            {
                DataInit.AddRecord(writer, phoneBook);
            }
            return phoneBook;
        }

        [HttpPut("UpdateRecord")]
        public void Put([FromBody] Record phoneBook)
        { 
            DataInit.UpdateRecord(binaryFile, phoneBook.Id, phoneBook);
        }

        [HttpDelete("DeleteRecord")]
        public void Delete([FromBody] int id) 
        {
            DataInit.DeleteRecord(binaryFile, id);
        }


        [HttpGet("AlphabeticalOrder")]
        public List<string> Sort()
        {
            return DataInit.SortRecords(binaryFile);
        }
    }
}
