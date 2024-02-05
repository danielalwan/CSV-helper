using System;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace CVShelper
{
    public class Program
    {
        static void Main(string[] args)
        {
            string sourceFilePath = @"C:\Users\Daniela.Alwan\csv.csv";
            string completeFolderPath = @"C:\Users\Daniela.Alwan\Completed";
            string errorFolderPath = @"C:\Users\Daniela.Alwan\Error";
            bool isError = false;

            var errorRecords = GetListFromFile(sourceFilePath, out isError);


            if (isError)
            {
                string errorFilePath = Path.Combine(errorFolderPath, Path.GetFileNameWithoutExtension(sourceFilePath) + "_errors.csv");
                WriteNewRecordsToFile(errorFilePath, errorRecords);
                Console.WriteLine("Error file created and moved to error folder");
            }
            else
            {
                File.Move(sourceFilePath, Path.Combine(completeFolderPath, Path.GetFileName(sourceFilePath)));
                Console.WriteLine("File moved to complete folder");
            }
        }

        public static List<StoreModel> GetListFromFile(string sourceFilePath, out bool isError)
        {
            isError = false;
            var errorList = new List<StoreModel>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = "|",
                IgnoreBlankLines = true,
                MissingFieldFound = null,
            };

            using (var reader = new StreamReader(sourceFilePath, Encoding.UTF8))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<StoreModelMap>();

                while (csv.Read())
                {
                    try
                    {
                        var store = csv.GetRecord<StoreModel>();

                        if (store.RegionId == null || !IsValidEmail(store.Email) || store.SiteId ==null)
                        {
                            isError = true;
                            errorList.Add(store);
                            continue;
                        }
                        
                    }
                    catch (CsvHelper.TypeConversion.TypeConverterException)
                    {
                        isError = true;
                        var errorRecord = new StoreModel
                        {
                            SiteId = csv.GetField<int?>("SiteId"),
                            Name = csv.GetField("Name"),
                            MainLedgerAccountNumber = csv.GetField<decimal?>("MainLedgerAccountNumber"),
                            OrganizationNumber = csv.GetField<decimal?>("OrganizationNumber"),
                            Email = csv.GetField("Email"),
                            ChainName = csv.GetField("ChainName"),
                            RegionId = csv.GetField<int?>("RegionId"),

                        }; errorList.Add(errorRecord);
                    }
                }
            }
            return errorList;
        }




        public static void WriteNewRecordsToFile(string sourceFilePath, List<StoreModel> records)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = "|",
                IgnoreBlankLines = true,
                MissingFieldFound = null,
            };
            using (var writer = new StreamWriter(sourceFilePath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.Context.RegisterClassMap<StoreModelMap>();

              
                csv.WriteRecords(records);
            }
        }

        public static bool IsValidEmail(string emailaddress)
        {
            if (String.IsNullOrEmpty(emailaddress))
            {
                return false;
            }

            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }


}

