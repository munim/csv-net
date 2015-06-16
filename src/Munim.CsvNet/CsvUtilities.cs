using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Munim.CsvNet.Attributes;

namespace Munim.CsvNet
{
    public class CsvUtilities
    {
        public static void WriteCsv<T>(IEnumerable<T> source, string csvFilePath)
        {
            var stringBuilder = new StringBuilder();

            object first = source.First();
            var propertySequence = new List<string>();
            foreach (PropertyInfo propertyInfo in first.GetType().GetProperties().OrderBy(p => (p.GetCustomAttributes(true).First(q => q is CsvAttribute) as CsvAttribute).ColumnIndex))
            {
                propertySequence.Add(propertyInfo.Name);
                stringBuilder.AppendFormat(@"""{0}"",", propertyInfo.Name);
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.AppendLine();

            foreach (var obj in source)
            {
                foreach (var propertyName in propertySequence)
                {
                    stringBuilder.AppendFormat(@"""{0}"",", obj.GetType().GetProperty(propertyName).GetValue(obj, null));
                }

                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.AppendLine();
            }

            File.WriteAllText(csvFilePath, stringBuilder.ToString());
        }

        public static List<T> LoadCsv<T>(string csvFilePath) where T : new()
        {
            string[] allRows = File.ReadAllLines(csvFilePath);

            IEnumerable<string> headers = allRows.First().Split(',').Select(p => p.Trim('"'));
            IEnumerable<IEnumerable<string>> allDataRows = allRows.Skip(1).Select(p => p.Split(',').Select(q => q.Trim('"')));

            var allParticipants = allDataRows.Select(p =>
            {
                var participant = new T();

                for (int i = 0; i < p.Count(); i++)
                {
                    var value = p.ElementAt(i);
                    var key = headers.ElementAt(i);

                    if (!string.IsNullOrEmpty(value))
                    {
                        Type propertyReturnType = participant.GetType().GetProperty(key.Replace(' ', '_')).GetGetMethod().ReturnType;
                        var propertyType = propertyReturnType.Name.Replace(propertyReturnType.Namespace + ".", string.Empty);
                        object covertedValue = typeof(Convert).GetMethod("To" + propertyType, BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder, new[] { typeof(object) }, null).Invoke(null, new[] { value });
                        participant.GetType().GetProperty(key.Replace(' ', '_')).SetValue(participant, covertedValue, null);
                    }
                }
                return participant;
            });

            return allParticipants.ToList();
        }
    }
}