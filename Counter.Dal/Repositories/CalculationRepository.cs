using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Counter.Domain.Helpers;
using Counter.Domain.Models;
using Counter.Domain.Repositories;

namespace Counter.Dal.Repositories
{
    public class CalculationRepository : ICalculationRepository
    {
        private readonly string _filePath = $"C:\\Users\\komaks58\\Source\\Repos\\Counter\\Counter.Api\\bin\\CalculationHistory.xml";
        private readonly IXmlSerializer _xmlSerializer;

        public CalculationRepository(IXmlSerializer xmlSerializer)
        {
            _xmlSerializer = xmlSerializer ?? throw new ArgumentNullException(nameof(xmlSerializer));
        }

        public async Task<int> GetCurrentTotal()
        {
            using (var fileStream = File.Open(_filePath,
                FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                var doc = XDocument.Load(fileStream);
                fileStream.Position = 0;

                var currentTotalValue = GetCurrentTotalValueRecord(doc);

                return currentTotalValue.TotalValue;
            }
        }

        public async Task UpdateTotalValue(TotalValueRecord newTotalValue)
        {
            using (var fileStream = File.Open(_filePath, 
                FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                var isEmpty = fileStream.Length == 0;

                XDocument doc;

                if (isEmpty)
                {
                    doc = XDocument.Parse($"<Calculations></Calculations>");

                    var newTotalValueXml = _xmlSerializer.Serialize(newTotalValue, typeof(TotalValueRecord));
                    doc.Root.Add(XElement.Parse(newTotalValueXml));
                }
                else
                {
                    doc = XDocument.Load(fileStream);
                    fileStream.Position = 0;

                    var currentTotalValue = GetCurrentTotalValueRecord(doc);

                    var newHistoryRecord = new HistoryRecord
                    {
                        CalculationOperationType = currentTotalValue.CalculationOperationType,
                        Value = currentTotalValue.Value
                    };

                    var currentTotalValueElement = doc.Root.Elements().First();

                    var historyRecordXml = _xmlSerializer.Serialize(newHistoryRecord, typeof(HistoryRecord));
                    currentTotalValueElement.AddAfterSelf(XElement.Parse(historyRecordXml));

                    var newTotalValueXml = _xmlSerializer.Serialize(newTotalValue, typeof(TotalValueRecord));
                    currentTotalValueElement.ReplaceWith(XElement.Parse(newTotalValueXml));
                }

                using (var xmlTextWriter = new XmlTextWriter(fileStream, new UTF8Encoding(false)))
                {
                    doc.Save(xmlTextWriter);
                }
            }
        }

        private TotalValueRecord GetCurrentTotalValueRecord(XDocument doc)
        {
            var currentTotalValueElement = doc.Root.Elements().First();

            var serializer = new XmlSerializer(typeof(TotalValueRecord));
            var currentTotalValue = (TotalValueRecord)serializer.Deserialize(currentTotalValueElement.CreateReader());

            return currentTotalValue;
        }
    }
}
