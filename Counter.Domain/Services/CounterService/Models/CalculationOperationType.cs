using System.ComponentModel;
using System.Xml.Serialization;

namespace Counter.Domain.Services.CounterService.Models
{
    public enum CalculationOperationType
    {
        [Description("+")]
        [XmlEnum("+")]
        Add = 1,
        [Description("-")]
        [XmlEnum("-")]
        Remove,
        [Description("*")]
        [XmlEnum("*")]
        Multiply,
        [Description("/")]
        [XmlEnum("/")]
        Divide
    }
}