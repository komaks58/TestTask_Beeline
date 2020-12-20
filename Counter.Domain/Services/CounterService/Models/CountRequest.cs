using System.Xml.Serialization;

namespace Counter.Domain.Services.CounterService.Models
{
    [XmlType("CountRequest")]
    public class CountRequest
    {
        [XmlElement("InputValue")]
        public int InputValue { get; set; }

        [XmlElement("CalculationType")]
        public CalculationOperationType CalculationType { get; set; }
    }
}