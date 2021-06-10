using Contract.Infra.Messaging;
using System;

namespace Contract
{
    public class NotAWeatherForecast : Event
    {
        public string SomethingElse { get; set; }
        public DateTime Date { get; set; }

        public string Summary { get; set; }
    }
}
