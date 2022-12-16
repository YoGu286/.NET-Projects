using PhoneBook.Models;
using System.Buffers.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace PhoneBook.DataAccess
{
    public static class DataInit
    {
        
        public static void SeedData(string binaryFile)
        {
            if (!File.Exists(binaryFile))
            {
                Record Record1 = new Record(1, "Tyler", "Bod", "Mobile", "6948225904");
                Record Record2 = new Record(2, "George", "Gkoumas", "Mobile", "6948225904");
                Record Record3 = new Record(3, "George", "Emmanouel", "Home", "2102335400");

                using (BinaryWriter writer = new BinaryWriter(new FileStream(binaryFile, FileMode.Create)))
                {
                    AddRecord(writer, Record1);
                    AddRecord(writer, Record2);
                    AddRecord(writer, Record3);
                }
            }
        }
        public static void AddRecord(BinaryWriter writer, Record Record)
        {
            writer.Write(Record.Id);
            writer.Write(Record.FirstName);
            writer.Write(Record.LastName);
            writer.Write(Record.Type);
            writer.Write(Record.PhoneNumber);  
        }

        public static int GetFileSize(string binaryFile)
        {
            FileInfo f = new FileInfo(binaryFile);

            return Convert.ToInt32(f.Length);
        }

        public static int FindRecordById(string binaryFile, int inputId)
        {
            int recPosition = -1;
            int readId = -1;
            int totalRecords = GetFileSize(binaryFile) / 88;    //each Object has a "fixed" size of 88 bytes(int+4*20char strings+terminating chars for each string)
            
            using (FileStream fileStream = new FileStream(binaryFile, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    for (int count = 0; count < totalRecords; count++)
                    {
                        recPosition = 88*count;                             //iterate through the records 

                        fileStream.Seek( recPosition, SeekOrigin.Begin);    

                        readId = reader.ReadInt32();

                        if (readId == inputId)
                        {
                            return recPosition;
                        }
                        else
                        {
                            recPosition = -1;
                        }
                    }
                }
            }
            return recPosition;
            
        }

        public static void UpdateRecord(string binaryFile, int inputId, Record rec)
        {
            int totalRecords = GetFileSize(binaryFile) / 88;
            int pos = FindRecordById(binaryFile, inputId);
            if (pos != -1)
            {
                using (FileStream fileStream = new FileStream(binaryFile, FileMode.Open))
                {
                    fileStream.Seek(pos + sizeof(int), SeekOrigin.Begin);
                    
                    using (BinaryWriter writer = new BinaryWriter(fileStream))
                    {
                        Record num = new(inputId, rec.FirstName, rec.LastName, rec.Type, rec.PhoneNumber); //here instead of updating the existing Record, we create a new Record with the new given values and we 
                                                                                                            //write it on top of the bytes of the existing one
                        writer.Write(num.FirstName);
                        writer.Write(num.LastName);
                        writer.Write(num.Type);
                        writer.Write(num.PhoneNumber);
                        
                    }
                }
            }
        }

        public static void DeleteRecord(string binaryFile, int inputId)
        {
            byte[] x = File.ReadAllBytes(binaryFile);
            byte[] temp = new byte[x.Length - 88]; 
            long tempx = 0;
            
            int pos = FindRecordById(binaryFile, inputId); //will give us the position of the Record to be deleted
            if (pos != -1)
            {
                    for(long i=0; i<pos; i++)   //we go through each Record until we hit our target...
                    {
                        temp[tempx] = x[i];
                        tempx++;
                    }
                    for(long i=pos+88; i<x.LongLength; i++) //...and then we skip it, writing the rest of the Records
                    {
                        temp[tempx] = x[i];
                        tempx++;
                    }
            }
            File.WriteAllBytes(binaryFile, temp);
        }

        public static List<string> SortRecords(string binaryFile)  
        {
            int totalRecords = GetFileSize(binaryFile) / 88;
            byte[] x = File.ReadAllBytes(binaryFile);
            byte[] temp = new byte[21];
            List<string> strs = new List<string>(); //we'll sort a list of strings, each string being another last name in our phonebook
            for (long i = 26; i <= GetFileSize(binaryFile); i+=88) //first last name is found in byte #26, and each Record takes up 88 bytes
            {
                for (long j = 0; j < 21; j++)        //each string has a size of 21 bytes(NameSize + terminating char)
                {
                    temp[j] = x[i+j];
                }
                strs.Add(Encoding.UTF8.GetString(temp));          //we add the last name that has been inserted in temp after converting it to a string
            }
            strs.Sort();
            return strs;
        }
        

       


    }
}
