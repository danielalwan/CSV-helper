using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CVShelper
{
    public class NullableIntConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
                return "NULL";
            }
            return value.ToString(); // Antag att value är en int.
        }

    }

    public class NullableDecimalConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
                return "NULL";
            }
            return value.ToString(); // Antag att value är en decimal.
        }

    }
}