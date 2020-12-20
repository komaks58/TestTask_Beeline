using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Counter.Domain.Services.CounterService.Models;

namespace Counter.Domain.Models
{
    [Serializable()]
    [XmlType("Current")]
    public class TotalValueRecord
    {
        [XmlElement("Action")]
        public CalculationOperationType CalculationOperationType { get; set; }

        [XmlElement("Data")]
        public int Value { get; set; }

        [XmlElement("Total")]
        public int TotalValue { get; set; }
    }
}
