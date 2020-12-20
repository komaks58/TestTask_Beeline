using System.Xml.Serialization;
using Counter.Domain.Services.CounterService.Models;

namespace Counter.Domain.Models
{
    [XmlType("Item")]
    public class HistoryRecord
    {
        [XmlElement("Action")]
        public CalculationOperationType CalculationOperationType { get; set; }

        [XmlElement("Data")]
        public int Value { get; set; }
    }
}
