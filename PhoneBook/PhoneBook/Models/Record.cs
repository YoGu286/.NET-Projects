using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PhoneBook.Models
{
    public class Record
    {
        [Key]
        public int Id { get; set; }
        
        public const int NameSize = 20; //every string will have a fixed size so that we can handle bytes in the binary file
        private string _firstName = String.Empty;
        private string _lastName = String.Empty;
        private string _type = String.Empty;
        private string _phoneNumber = String.Empty;
        public string FirstName
        {
            get
            {
                return (_firstName.Length > NameSize) ? _firstName.Substring(0, NameSize) : _firstName.PadRight(NameSize);
            }
            set
            {
                _firstName = value;
            }

        }
        public string LastName
        {
            get
            {
                return (_lastName.Length > NameSize) ? _lastName.Substring(0, NameSize) : _lastName.PadRight(NameSize);
            }
            set
            {
                _lastName = value;
            }
        }
        public string Type
        {
            get 
            { 
                return(_type.Length > NameSize) ? _type.Substring(0, NameSize) : _type.PadRight(NameSize); 
            }
            set 
            { 
                _type = value; 
            }
        }
        public string PhoneNumber
        {
            get 
            { 
                return (_phoneNumber.Length > NameSize) ? _phoneNumber.Substring(0, NameSize) : _phoneNumber.PadRight(NameSize); 
            }
            set 
            { 
                _phoneNumber = value; 
            }   
        }
        public Record(int id, string firstName, string lastName, string type, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Type = type;
            PhoneNumber = phoneNumber;
        }
    }
}
